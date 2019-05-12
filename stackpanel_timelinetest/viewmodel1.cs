using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Globalization;
using ReactiveUI;
using System.Windows;
using System.Windows.Data;


namespace stackpanel_timelinetest
{
    public class EventLengthConverter : IValueConverter
    {

        public object Convert(object values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {

            if (targetType == typeof(Thickness))
            {
                return new Thickness(Double.Parse(values.ToString()), 0, 0, 0);
            }
            else
            {
                return values;
            }
        }

        public object ConvertBack(object value, Type targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class TimeEvent
    {
        public int durationInMs { get; set; }
        public int offsetInMs { get; set; }
        public DateTime eventTime { get; set; }
    }

    class viewmodel1 : ReactiveObject
    {
        public ObservableCollection<TimeEvent> TimeEventsCollection { get; set; }
        public Queue<TimeEvent> TimeEventQueue { get; set; }
        
        private void _refreshOffset()
        {
            foreach (TimeEvent q in TimeEventQueue )
            {
                q.offsetInMs = (q.eventTime - TimeEventQueue.FirstOrDefault().eventTime).Milliseconds;
            }
        }

        public viewmodel1()
        {
            TimeEventQueue = new Queue<TimeEvent>();
            TimeEventQueue.Enqueue(new TimeEvent { durationInMs = 10, eventTime = DateTime.Now });
            TimeEventQueue.Enqueue(new TimeEvent { durationInMs = 70, eventTime = DateTime.Now.AddMilliseconds(20) } );
            TimeEventQueue.Enqueue( new TimeEvent { durationInMs = 20, eventTime = DateTime.Now.AddMilliseconds(100) } );
            TimeEventQueue.Enqueue(new TimeEvent { durationInMs = 50, eventTime = DateTime.Now.AddMilliseconds(200) } );
            TimeEventQueue.Enqueue(new TimeEvent { durationInMs = 30, eventTime = DateTime.Now.AddMilliseconds(300) } );

            _refreshOffset();
        }
    }
}
