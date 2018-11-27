using LK_Teacher.Modules.Utility;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LK_Teacher.Modules.Models
{
    class ProfileModel : BindableBase
    {
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { SetProperty(ref _Email, value); }
        }

        private string _LName;
        public string LName
        {
            get { return _LName; }
            set { SetProperty(ref _LName, value); }
        }

        private string _FName;
        public string FName
        {
            get { return _FName; }
            set { SetProperty(ref _FName, value); }
        }

        private string _MName;
        public string MName
        {
            get { return _MName; }
            set { SetProperty(ref _MName, value); }
        }

        private string _Direction;
        public string Direction
        {
            get { return _Direction; }
            set { SetProperty(ref _Direction, value); }
        }

        private string _PhoneNumber;
        public string PhoneNumber
        {
            get { return _PhoneNumber; }
            set { SetProperty(ref _PhoneNumber, value); }
        }

        private string _Quote;
        public string Quote
        {
            get { return _Quote; }
            set { SetProperty(ref _Quote, value); }
        }
        private string _FullName;
        public string FullName
        {
            get { return _FullName; }
            set { SetProperty(ref _FullName, value); }
        }

        public ObservableCollection<CheckBox> SubjectList;

        private DateTime _DateBirth;
        public DateTime DateBirth
        {
            get { return _DateBirth; }
            set { SetProperty(ref _DateBirth, value); }
        }

        public ProfileModel()
        {
            SubjectList = new ObservableCollection<CheckBox>();
            Initialize();
        }

        private void Initialize()
        {
            Hashtable ht = DBApi.GetTeacherData(UserModel.IdTeacher);

            if (ht.Count != 0)
            {
                Email = ht["email_teacher"] as string;
                FName = ht["fname_teacher"] as string;
                LName = ht["lname_teacher"] as string;
                MName = ht["mname_teacher"] as string;
                FullName = $"{LName} {FName} {MName}";
                Direction  = ht["name_direction"] as string;
                PhoneNumber = ht["phone_number_teacher"] as string;
                Quote = ht["quote_teacher"] as string;

                if (ht["date_birth_teacher"].ToString() != "")
                {
                    DateBirth = DateTime.Parse(ht["date_birth_teacher"] as string);
                }
            }

            List<Hashtable> list = DBApi.GetSubjects(UserModel.DirectionTeacher);
            if (list.Count != 0)
            {
                foreach (Hashtable hashtable in list)
                {
                    int id_subject = Convert.ToInt32(hashtable["id_subject"]);
                    CheckBox ch = new CheckBox()
                    {
                        Content = hashtable["name_subject"],
                        IsChecked = DBApi.HasSubject(UserModel.IdTeacher, id_subject)
                    };

                    SubjectList.Add(ch);
                }
            }
        }
    }
}
