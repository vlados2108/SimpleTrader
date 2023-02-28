using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTrader.WPF.ViewModels
{
    public class MajorIndexListingViewModel:ViewModelBase
    {
        private readonly IMajorIndexService majorIndexService;

        private MajorIndex? apple;

        public MajorIndex? Apple
        {
            get
            {
                return apple;
            }
            set
            {
                apple = value;
                OnPropertyChanged(nameof(Apple));
            }
        }

        private MajorIndex? google;
        public MajorIndex? Google
        {
            get
            {
                return google;
            }
            set
            {
                google = value;
                OnPropertyChanged(nameof(Google));
            }
        }

        private MajorIndex? meta;
        public MajorIndex? Meta
        {
            get
            {
                return meta;
            }
            set
            {
                meta = value;
                OnPropertyChanged(nameof(Meta));
            }
        }


        public MajorIndexListingViewModel(IMajorIndexService majorIndexService)
        {
            this.majorIndexService = majorIndexService;
        }

        public static MajorIndexListingViewModel LoadMajorIndexViewModel(IMajorIndexService majorIndexService)
        {
            MajorIndexListingViewModel majorIndexViewModel = new MajorIndexListingViewModel(majorIndexService);
            majorIndexViewModel.LoadMajorIndexex();
            return majorIndexViewModel;
        }

        private void LoadMajorIndexex()
        {
            majorIndexService.GetMajorIndex(MajorIndexType.Apple).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Apple = task.Result;
                }
            });
            majorIndexService.GetMajorIndex(MajorIndexType.Google).ContinueWith(task =>
            {
                if (task.Exception == null)
                {
                    Google = task.Result;
                }
            });
            majorIndexService.GetMajorIndex(MajorIndexType.Meta).ContinueWith(task => 
            {
                if (task.Exception == null) 
                { 
                    Meta = task.Result;
                } 
            });
        }
    }
}
