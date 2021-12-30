using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTCG
{
    class Friendrequest
    {

        public int _friendid { get; set; }
        public int _thisuserid { get; set; }
        public int _otheruserid { get; set; }

        public Friendrequest(int friendid, int thisuserid, int otheruserid)
        {
            _friendid = friendid;
            _thisuserid = thisuserid;
            _otheruserid = otheruserid; 
        }

        public void PrintFriendrequest()
        {
            Database db = Database.getInstance();

            Console.WriteLine($"Friendrequest by {db.getUsernameByUserID(_thisuserid)}\n");
        }

    }
}
