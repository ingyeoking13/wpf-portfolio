using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

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

    // 코드포스 콘테스트
    public class Contest
    {
        public int id { get; set; }
        public string name { get; set; }
        public int durationSeconds { get; set; }
        public int startTimeSeconds { get; set; }
    }
    public class ContestAPI
    {
        public string status { get; set; }
        public List<Contest> result {get; set;}
    }

/*
id Integer.
name    String.Localized.
type Enum: CF, IOI, ICPC.Scoring system used for the contest.
phase Enum: BEFORE, CODING, PENDING_SYSTEM_TEST, SYSTEM_TEST, FINISHED.
frozen Boolean. If true, then the ranklist for the contest is frozen and shows only submissions, created before freeze.
durationSeconds Integer. Duration of the contest in seconds.
startTimeSeconds Integer. Can be absent.Contest start time in unix format.
relativeTimeSeconds Integer. Can be absent.Number of seconds, passed after the start of the contest.Can be negative.
preparedBy String. Can be absent.Handle of the user, how created the contest.
websiteUrl String. Can be absent.URL for contest-related website.
description String. Localized.Can be absent.
difficulty Integer. Can be absent.From 1 to 5. Larger number means more difficult problems.
kind String. Localized.Can be absent.Human-readable type of the contest from the following categories: Official ICPC Contest, Official School Contest, Opencup Contest, School/University/City/Region Championship, Training Camp Contest, Official International Personal Contest, Training Contest.
icpcRegion String. Localized.Can be absent.Name of the Region for official ICPC contests.
country String. Localized.Can be absent.
city String. Localized.Can be absent.
season String. Can be absent.
*/

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


        public async Task<IEnumerable<Contest>> GetAllContest()
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create("https://codeforces.com/api/contest.list");
            var requestResult = (HttpWebResponse)(await httpWebRequest.GetResponseAsync());
            if (requestResult.StatusCode != HttpStatusCode.OK)
                return null;

            using (var stream = new StreamReader(requestResult.GetResponseStream()))
            {
                var str = stream.ReadToEnd();
                var result = JsonConvert.DeserializeObject<ContestAPI>(str);
                return result.result;
            }
        }

        // Fake Repository
        List<User> _users = new List<User>
        {
            new User
            {
                Id = new Guid(),
                Name = "정요한",
                Description = "",
                ProfilePicture = "/DummyImages/Yohan.jpg",
                Point = 1.7f
            },
            new User
            {
                Id = new Guid(),
                Name = "정문희",
                Description = "",
                ProfilePicture = "/DummyImages/Munee.jpg",
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
