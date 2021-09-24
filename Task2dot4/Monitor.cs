

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Timers;

namespace Task2dot4
{
    public class Monitor : IDisposable
    {
        private const int MILISECONDS = 1000;

        const string CONFIG_FILE_PATH = @"C:\Users\PauliusRuikis\Desktop/config.json";

        const string WATCHER_FILE_PATH = @"C:\Users\PauliusRuikis\Desktop/";

        private Timer _siteCheckTimer;

        private TextLogger _logger;

        private List<Timer> __siteCheckTimers;

        private FileSystemWatcher _watcher;
        private bool disposedValue;

        public List<Site> SitesList { get; private set; }
        public Monitor( List<Site> sitesList ) 
        {
            SitesList = sitesList.Select( site => new Site (site.Interval, site.MaxResponseTime, site.Url, site.AdminEmail) ).ToList();
            _watcher = new FileSystemWatcher();
            _watcher.Path = WATCHER_FILE_PATH;
            _watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            _watcher.Filter = "config.json";
            _watcher.Changed += OnChanged;
            _watcher.EnableRaisingEvents = true;
        }
        private bool IsSiteAvailable( Site site )
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(site.Url);
                request.Timeout = site.MaxResponseTime* MILISECONDS;
                request.AllowAutoRedirect = false; 
                request.Method = "HEAD";

                using (var response = request.GetResponse())
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        public void InitializeChecking()
        {
            _logger = new TextLogger();
            __siteCheckTimers = new List<Timer>();
            foreach ( var site in SitesList )
            {
                _siteCheckTimer = new Timer();
                _siteCheckTimer.Interval = site.Interval * MILISECONDS;
                _siteCheckTimer.Elapsed += ( sender, e ) =>
                {
                    if (IsSiteAvailable( site ))
                    {

                        _logger.LogInfo($"ok {site.Url}");
                    }
                    else
                    {
                       EmailSender.SendEmail( site.AdminEmail );
                    }
                };
                __siteCheckTimers.Add( _siteCheckTimer );
            }
        }
        public void StartChecking()
        {
            foreach ( var timer in __siteCheckTimers )
            {
                timer.Start();
            }
        }
        private void StopChecking()
        {
            foreach ( var timer in __siteCheckTimers )
            {
                timer.Stop();
                timer.Dispose();
            }
        }
        private void OnChanged( object sender, FileSystemEventArgs e )
        {
            StopChecking();
            this.SitesList = null;
            this.SitesList = JSONReader.GetSitesFromConfig(CONFIG_FILE_PATH);
            this.InitializeChecking();
            this.StartChecking();
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
