using LK_Teacher.Modules.Utility;
using LK_Teacher.Utility;
using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Prism.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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

        private string _ImagePath;
        public string ImagePath
        {
            get { return _ImagePath; }
            set { SetProperty(ref _ImagePath, value); }
        }

        private string _Education;
        public string Education
        {
            get { return _Education; }
            set { SetProperty(ref _Education, value); }
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
        public ObservableCollection<CheckBox> DirectionList;

        private DateTime _DateBirth;
        public DateTime DateBirth
        {
            get { return _DateBirth; }
            set { SetProperty(ref _DateBirth, value); }
        }

        public ProfileModel()
        {
            SubjectList = new ObservableCollection<CheckBox>();
            DirectionList = new ObservableCollection<CheckBox>();
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
                //Можно сделать кэширование изображения но тут надо подуммать и погуглить
                if (ht["image_profile_teacher"] as string != "")
                {
                    ImagePath = Properties.Settings.Default.ImagesUrl + ht["image_profile_teacher"] as string;
                }
                else ImagePath = "";


                PhoneNumber = ht["phone_number_teacher"] as string;
                Education = ht["education_teacher"] as string;
                Quote = ht["quote_teacher"] as string;

                if (ht["date_birth_teacher"].ToString() != "")
                {
                    DateBirth = DateTime.Parse(ht["date_birth_teacher"] as string);
                }
            }

            DirectionList.Clear();

            List<Hashtable> list = DBApi.GetDirections();
            if (list.Count != 0)
            {
                foreach (Hashtable hashtable in list)
                {
                    int id_direction = Convert.ToInt32(hashtable["id_direction"]);
                    CheckBox ch = new CheckBox()
                    {
                        Tag = id_direction,
                        Content = hashtable["name_direction"],
                        IsChecked = DBApi.HasDirection(UserModel.IdTeacher, id_direction)
                    };

                    ch.Click += IsCheackedEventDirection;

                    DirectionList.Add(ch);
                }
            }

            UpdateSubjects();
        }

        public void NewPhoto()
        {
            string path = UtilFunctions.OpenImage();

            if (path != "")
            {
                HttpClient client = new HttpClient();

                MultipartFormDataContent form = new MultipartFormDataContent();

                var image = File.ReadAllBytes(path);

                HttpContent content = new StreamContent(new MemoryStream(image));

                form.Add(content, "image", path);

                HttpResponseMessage response = null;

                try
                {
                    response = (client.PostAsync("http://lk-teacher.loc/api/set_image_teacher.php", form)).Result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                var serialize_response = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);

                if (serialize_response.FileName != null)
                {
                    DBApi.UpdateImage(serialize_response.FileName, UserModel.IdTeacher);
                    Initialize();
                }
                else
                {
                    //попап
                }
            }

        }

        private void IsCheackedEventDirection(object sender, RoutedEventArgs e)
        {
            var ch = sender as CheckBox;
            int id_direction = Convert.ToInt32(ch.Tag);

            if (!(bool)ch.IsChecked)
            {
                List<Hashtable> list = DBApi.GetSubjects(UserModel.IdTeacher, id_direction);
                if (list.Count != 0)
                {
                    foreach (Hashtable hashtable in list)
                    {
                        int id_subject = Convert.ToInt32(hashtable["id_subject"]);
                        DBApi.DeleteSubject(UserModel.IdTeacher, id_subject);
                    }
                }
            }

            DBApi.SetUnsetDirection(UserModel.IdTeacher, id_direction, (bool)ch.IsChecked);

            UpdateSubjects();
        }

        private void UpdateSubjects()
        {
            SubjectList.Clear();

            List<Hashtable> list = DBApi.GetSubjects(UserModel.IdTeacher);
            if (list.Count != 0)
            {
                foreach (Hashtable hashtable in list)
                {
                    int id_subject = Convert.ToInt32(hashtable["id_subject"]);
                    CheckBox ch = new CheckBox()
                    {
                        Content = hashtable["name_subject"],
                        IsChecked = DBApi.HasSubject(UserModel.IdTeacher, id_subject),
                        Tag = id_subject
                    };
                    ch.Click += IsCheckedEventSubject;

                    SubjectList.Add(ch);
                }
            }
        }

        private void IsCheckedEventSubject(object sender, RoutedEventArgs e)
        {
            var ch = sender as CheckBox;
            int id_subject = Convert.ToInt32(ch.Tag);

            DBApi.SetUnsetSubject(UserModel.IdTeacher, id_subject, (bool)ch.IsChecked);
        }

        public void SaveChanges()
        {
            string FormatedPhoneNumber = UtilFunctions.FormatPhoneNumber(PhoneNumber);

            try
            {
                DBApi.UpdateTeacherProfile(UserModel.IdTeacher, FName, LName, MName, FormatedPhoneNumber, DateBirth, Education, Quote);

            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
            Initialize();
        }
    }

    class Response
    {
        public string FileName { get; set; }
    }
}
