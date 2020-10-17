using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace WPF_PortFolio.Utils
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ProfilePicture { get; set; }
        public float Point { get; set; }
    }

    public class Portfolio
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Date { get; set; }
    }

    public class FakeRepository
    {
        static private FakeRepository _instance;
        static public FakeRepository Instance {
            get {
                if (_instance == null) 
                    return _instance = new FakeRepository();
                else 
                    return _instance;
            }
        }

        private FakeRepository()
        {

        }
        public IEnumerable<User> GetAllUser()
        {
            return _users;
        }

        public IEnumerable<User> GetUserByName(string name)
        {
            return _users.Where(u => u.Name == name);
        }

        public IEnumerable<Portfolio> GetAllPortfolios()
        {
            return _portfolios;
        }

        // Fake Repository
        List<User> _users = new List<User>
        {
            new User
            {
                Id = new Guid(),
                Name = "정요한",
                Description = "",
                ProfilePicture = "/DummyImages/Yohan.png",
                Point = 1.7f
            },
            new User
            {
                Id = new Guid(),
                Name = "정문희",
                Description = "",
                ProfilePicture = "/DummyImages/Munee.png",
                Point = 5f
            },
            new User
            {
                Id = new Guid(),
                Name = "이말년",
                Description = "",
                ProfilePicture = "/DummyImages/imalnyun.png",
                Point = 3.5f,
            },
            new User
            { 
                Id = new Guid(),
                Name = "유재석" ,
                Description = "",
                ProfilePicture = "/DummyImages/JaeSuk.png",
                Point = 4f
            }
        };

        List<Portfolio> _portfolios = new List<Portfolio>
        {
            new Portfolio
            {
                Title = "A.Works",
                Image = "/DummyImages/AWORKS.png",
                URL = "https://aworksrpa.com/",
                Description = "ABCDEFGTIWQMERIQWMEILQWMELQWMEL",
                Date = "2020.02 ~ 2020.08"
            },
            new Portfolio
            {
                Title = "Vigne",
                Image = "/DummyImages/Vigne.png",
                URL = "https://ingyeoking13.tistory.com/259",
                Description = "와인 정보 공유 어플리케이션",
                Date = "2020.07 ~ ",
            },
            new Portfolio
            { 
                Title = "Chaos",
                Image ="",
                URL = "https://github.com/purplue-blue-develop/hongjun-android",
                Description = "건축물 결함 조사 기록 어플리케이션",
                Date = "2020.02 ~ 2020.04",
            },
            new Portfolio
            {
                Title = "Algorithm Study",
                Image = "",
                URL = "",
                Description = "",
                Date = ""
            }
        };
    }
}
