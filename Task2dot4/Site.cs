

using System;

namespace Task2dot4
{
    public class Site
    {
        private int _interval;

        private int _maxResponseTime;

        private string _url;

        private string _adminEmail;
        public Site( int interval, int maxResponseTime, string url, string adminEmail )
        {
            if ( interval > maxResponseTime )
            {
                _interval= interval;
                _maxResponseTime = maxResponseTime;
                _url = url;
                _adminEmail = adminEmail;
            }
            else
            {
                throw new ArgumentException("Response time can not be longer than interval!!!");
            }
        }
        public int Interval
        {
            get
            {
                return _interval;
            }
            set
            {
                if ( _interval >= 0 )
                {
                    _interval = value;
                }
                else
                {
                    throw new ArgumentException("You can not set time interval to negative value!!!");
                }
            }
        }
        public int MaxResponseTime
        {
            get
            {
                return _maxResponseTime;
            }
            set
            {
                if ( _maxResponseTime >= 0 )
                {
                    _maxResponseTime = value;
                }
                else
                {
                    throw new ArgumentException("You can not set response time  to  negative value!!!");
                }
            }
        }
        public string Url
        {
            get
            {
                return _url;
            }
            private set
            {
                if ( !String.IsNullOrEmpty(_url) )
                {
                    _url = value;
                }
                else
                {

                  throw new ArgumentException("Url can not be null or empty!!!");
                }
            }
        }
        public string AdminEmail 
        {
            get
            {
                return _adminEmail;
            }
            private set
            {
                if (!String.IsNullOrEmpty(_adminEmail))
                {
                    _adminEmail = value;
                }
                else
                {
                    throw new ArgumentException("Admin email can not be null or empty!!!");
                }
            }
        }


    }
}
