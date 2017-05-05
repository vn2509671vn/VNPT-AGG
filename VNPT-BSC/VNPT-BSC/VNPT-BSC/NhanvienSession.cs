using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;

namespace VNPT_BSC
{
    public static class NhanvienSession
    {
        public static void SetCurrentUser(this HttpSessionState session, Nhanvien nhanvien)
        {
            session["currentUser"] = nhanvien;
            session.Timeout = 90;
        }

        public static Nhanvien GetCurrentUser(this HttpSessionState session)
        {
            return session["currentUser"] as Nhanvien;
        }

        public static void SetRole(this HttpSessionState session, List<int> role)
        {
            session["role"] = role;
            session.Timeout = 90;
        }

        public static List<int> GetRole(this HttpSessionState session)
        {
            return session["role"] as List<int>;
        }
    }
}