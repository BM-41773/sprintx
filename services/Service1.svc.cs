using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Web.UI.WebControls;



namespace mysrves
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : IService1
    {
        public void DoWork()
        {
        }
        public string Login(string X_emailaddress, string X_passwd)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from loginuser where Email='" + X_emailaddress + "' and supPassword='" + X_passwd + "'";
            //  cmd.Dispose();
            //con.Close();
            SqlDataReader rdr = cmd.ExecuteReader();
            Logginuser logg = new Logginuser();
            logg.Emailadress = X_emailaddress;
            logg.Password = X_passwd;
            try
            {
                while (rdr.Read())
                {
                    if ((logg.Emailadress.Equals(rdr["Email"].ToString())) && (logg.Password.Equals(rdr["supPassword"].ToString())))
                    {
                        logg.Firstname = rdr["Firstname"].ToString();
                        logg.Lastname = rdr["Surname"].ToString();
                        logg.Emailadress = rdr["Email"].ToString();
                        logg.LoginID = Convert.ToInt32(rdr["LogginId"].ToString());
                        logg.X_LoginID = logg.LoginID.ToString();
                        logg.Profilepic = rdr["Profilepic"].ToString();
                        string str = "LoginId:" + logg.LoginID + "Firstname:" + logg.Firstname + "Surname:" + logg.Lastname + "Profilpic:" + logg.Profilepic + "Responsecode:200 Message:success";
                        return str;


                    }

                    else if ((logg.Emailadress.Equals(rdr["Email"].ToString())) && (!(logg.Password.Equals(rdr["suppassword"].ToString()))))
                    {
                        logg.Firstname = rdr["Firstname"].ToString();
                        logg.Lastname = rdr["Surname"].ToString();
                        logg.Emailadress = rdr["Email"].ToString();
                        logg.LoginID = Convert.ToInt32(rdr["LogginId"].ToString());
                        logg.X_LoginID = logg.LoginID.ToString();
                        logg.Profilepic = rdr["Profilepic"].ToString();
                        string str = "Responsecode;404 Message;Password incorrect";
                        return str;
                    }
                    else if (!(logg.Emailadress.Equals(rdr["Email"].ToString())) && ((logg.Password.Equals(rdr["supPassword"].ToString()))))
                    {
                        logg.Firstname = rdr["Firstname"].ToString();
                        logg.Lastname = rdr["Surname"].ToString();
                        logg.Emailadress = rdr["Email"].ToString();
                        logg.LoginID = Convert.ToInt32(rdr["LogginId"].ToString());
                        logg.X_LoginID = logg.LoginID.ToString();
                        logg.Profilepic = rdr["Profilepic"].ToString();

                        string str = "\"Username:\"" + logg.Emailadress + " Responsecode;500 Message;Email id doesnot exist";
                        return str;
                    }
                }
            }
            catch (Exception ec)
            {

                // SendError(ec);
            }

            return null;

        }
        public void loginId(string X_Email, string X_password, out string temp, out string X_logginid, out string X_F, out string X_Snme, out string X_prop)
        {
            temp = "";
            X_F = "";
            X_Snme = "";
            X_prop = "";
            X_password = "";
            X_logginid = "";

            SqlConnection cn = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            cn.Open();
            SqlCommand md = cn.CreateCommand();
            md.CommandText = "select LogginId,Firstname,Surname,Profilepic from loginuser  where Email= '" + X_Email + "'";
            SqlDataReader rdr = md.ExecuteReader();


            while (rdr.Read())
            {
                X_logginid = rdr["LogginId"].ToString();
                X_F = rdr["Firstname"].ToString();
                X_Snme = rdr["Surname"].ToString();
                X_prop = rdr["Profilepic"].ToString();


            }

        }



        string emil;
        public string Signupmethod(string X_Firstname, string X_Surname, string X_propic, string X_Email, string X_supPassword, DateTime DT_SUPdob, string x_Gender)
        {
            FileUpload FL1 = new FileUpload();
            String myprofpic = "D:\\profpic\\" + X_propic;
            FL1.SaveAs(myprofpic);

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-46KB8VH\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            string str;
            con.Open();
            signup sign = new signup();
            sign.Firstname = X_Firstname;
            sign.Lastname = X_Surname;

            sign.Profilepic = myprofpic;
            sign.Email = X_Email;

            sign.supPassword = X_supPassword;
            sign.DT_dob = DT_SUPdob;
            sign.Gender = x_Gender;
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = " select email from Bang_user  where email" + X_Email;
            cmd.ExecuteNonQuery();
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                emil = rdr["email"].ToString();
            }
            con.Close();
            int count = cmd.ExecuteNonQuery();
            if (count > 0)
            {



                cmd.CommandText = "insert into loginuser(Firstname,Surname,Profilepic,Message,supPassword,Email,DT_dob,Gender,Accountstatus) values('" + sign.Firstname + "', '" + sign.Lastname + "','" + sign.Profilepic + "','hi', '" + sign.supPassword + "' ,'" + sign.Email + "','" + sign.DT_dob + "', '" + sign.Gender + "',0)";
                // SqlCommand snd = new SqlCommand(str, con);
                int res = cmd.ExecuteNonQuery();
                if (res > 0)
                {


                    sendmail();



                    return ("success");
                }
                else
                {
                    return ("fail");
                }
                con.Close();
                cmd.Dispose();

            }
            return null;
        }

        public void sendmail()
        {



            //string mail = Frommail.Text;
            // string fun = Tomail.Text;
            //string sub = Subjectmail.Text;
            // string bdy = documail.Text;


            MailMessage msg = new MailMessage();
            msg.From = new MailAddress("snehasne517@gmail.com");
            msg.To.Add("snehasne517@gmail.com");
            //msg.Subject = sub;
            // msg.Body = bdy;
            msg.IsBodyHtml = true;

            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new NetworkCredential("snehasne517@gmail.com", "snehaanil");
            smtp.EnableSsl = true;
            try
            {
                smtp.Send(msg);
            }



            catch (Exception ex)
            {

            }
        }
        public string serchtype(string X_Firstname)
        {
            // int N_loginid = Convert.ToInt32(x_logginid);
            SqlConnection co = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            co.Open();
            SqlCommand cn = co.CreateCommand();
            //cn.CommandText="select Accountstatus from loginuser   where N_logginid ='"+N_loginid+"'" ;
            //SqlDataReader sdr = cn.ExecuteReader();
            //int Accountstatus = 0;
            //while(sdr.Read())
            //{
            //    Accountstatus = Convert.ToInt32(sdr["N_logginid"].ToString());

            //}
            cn.CommandText = "select  loginuser.LogginId,loginuser.Firstname,loginuser.Surname,loginuser.Profilepic,frndconform.N_logginid,frndconform.relation,frndconform.sender,frndconform.FCFirstname,frndconform.FCSurname,frndconform.FCProfilepic,frndconform.reciver from loginuser LEFT OUTER JOIN  frndcoform on loginuser.LogginId=frndconform.reciver where loginuser.Firstname like '" + X_Firstname + "%' order by frndconform.reciver desc ";
            //cn.CommandText = "select Firstname,Surname,Profilepic from loginuser where  Firstname like'" + X_Firstname + "%'";
            //  cn.CommandText = "select loginuser.LogginId,loginuser.Firstname,loginuser.Surname,loginuser.Profilepic,frndconform.N_logginid,frndcomform.relation,frndconform.sender,frondconform.FCFirstname,frndconform.FCSurname,frndconform.FCProfilepic,frndconform,reciver from loginuser LEFT OUTER JOIN frndcoform on loginuser.LogginId=frndconform.reciver where loginuser.Firtname like '" + X_Firstname + "%' order by frndconform.reciver desc";
            SqlDataReader rdr = cn.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;

            //string f1 = rdr["Firstname"].ToString();
            //string s1 = rdr["Surname"].ToString();
            //string p1 = rdr["Profilepic"].ToString();
            // return "select Firstname,Surname,Profilepic from Userlogin where  Firstname='" + X_Firstname + "'";


        }

        public string datatabletojson(DataTable table)
        {
            return JsonConvert.SerializeObject(table);
        }

        public void frnds(string X_reciverid, string X_senderid)
        {
            int N_reciverid = Convert.ToInt32(X_reciverid);
            int N_senderid = Convert.ToInt32(X_senderid);

            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-46KB8VH\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");

            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "update  loginuser set  Accountstatus=2 where LogginId=" + N_senderid;
            cnd.ExecuteNonQuery();
            cnd.CommandText = "select Firstname,Surname,Profilepic from  loginuser where LogginId='" + N_reciverid + "'";
            SqlDataReader rdr = cnd.ExecuteReader();
            string fname = "";
            string sname = "";
            string propic = "";
            while (rdr.Read())
            {
                fname = rdr["Firstname"].ToString();
                sname = rdr["Surname"].ToString();
                propic = rdr["Profilepic"].ToString();
            }
            rdr.Close();
            cnd.CommandText = "insert into frndconform (N_logginid,relation,sender,FCFirstname,FCSurname,FCProfilepic,reciver,FCstatus)values('" + X_senderid + "' ,'" + 0 + "','" + X_senderid + "','" + fname + "','" + sname + "','" + propic + "','" + X_reciverid + "','" + "" + "')";
            cnd.ExecuteNonQuery();


        }
        public void conform(string X_senderid, string X_reciverid)
        {
            int N_senderid = Convert.ToInt32(X_senderid);
            int N_reciverid = Convert.ToInt32(X_reciverid);
            SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-46KB8VH\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "update  frndconform set FCstatus=1 where sender=" + N_senderid + " and reciver=" + N_reciverid;
            cnd.ExecuteNonQuery();

            //public string delt(string X_reciverid)
            //{
            //   // int N_senderid = Convert.ToInt32(X_senderid);
            //    int N_reciverid = Convert.ToInt32(X_reciverid);
            //    SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            //    con.Open();
            //    SqlCommand cnd = con.CreateCommand();
            //    cnd.CommandText = "update frndconform set FCstatus=1 where reciver=" + X_reciverid;
            //    cnd.ExecuteNonQuery();

            mailconform(X_reciverid);
        }
        public string approvelfrnd(string X_senderid, string X_reciverid)
        {
            int N_reciverid = Convert.ToInt32(X_reciverid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "select loginuser.LogginId,loginuser.Firstname,loginuser.Surname,loginuser.Profilepic,frndconform.relation,frndconform.FCstatus from loginuser LEFT OUTER JOIN frndconform on frndconform.reciver= " + N_reciverid + " where loginuser.LogginId=frndconform.sender order by frndconform.reciver desc";
            SqlDataReader sdr = cnd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sdr);
            string str = datatabletojson(dt);
            return str;

        }
        string fname, sname, propics;
        public void mailconform(string X_reciverid)
        {
            int N_reciverid = Convert.ToInt32(X_reciverid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "select Firstname,Surname, Profilepic from loginuser where LogginId=" + N_reciverid;
            SqlDataReader sdr = cnd.ExecuteReader();
            while (sdr.Read())
            {
                fname = sdr["Firstname"].ToString();
                sname = sdr["Surname"].ToString();
                propics = sdr["Profilepic"].ToString();
                MailMessage msg = new MailMessage();
                msg.From = new MailAddress("snehasne517@gmail.com");
                msg.To.Add("harishmahari1502@gmail.com");
                //msg.Subject = sub;
                // msg.Body = bdy;
                msg.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = "smtp.gmail.com";
                smtp.Credentials = new NetworkCredential("snehasne517@gmail.com", "snehaanil");
                smtp.EnableSsl = true;
                try
                {
                    smtp.Send(msg);
                }



                catch (Exception ex)
                {

                }
            }

        }
        public void dlete(string X_reciverid, string X_senderid)
        {
            int N_reciverid = Convert.ToInt32(X_reciverid);
            int N_senderid = Convert.ToInt32(X_senderid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "delete from frndconform where reciver=" + N_reciverid + " and sender=" + N_senderid;

            cnd.ExecuteNonQuery();
        }
        public string frndsrch(string loginid)
        {
            int Nloginid = Convert.ToInt32(loginid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "select loginuser.LogginId,loginuser.Firstname,loginuser.Surname,loginuser.Profilepic,frndconform.relation,frndconform.sender,frndconform.reciver, frndconform.FCFirstname,frndconform.FCSurname,frndconform.FCProfilepic from loginuser LEFT OUTER JOIN frndconform on loginuser.LogginId = frndconform.reciver where frndconform.reciver=" + Nloginid + "   order by frndconform.reciver desc";
            // cnd.Connection = con; //SqlDataReader sdr = cnd.ExecuteReader();
            SqlDataAdapter adr = new SqlDataAdapter(cnd);
            DataTable dt = new DataTable();
            adr.Fill(dt);
            string str = datatabletojson(dt);
            return str;
        }
        public void unfrnd(string X_reciverid, string X_senderid)
        {
            int Nreciverid = Convert.ToInt32(X_reciverid);
            int Nsenderid = Convert.ToInt32(X_senderid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "delete from frndconform where reciver=" + Nreciverid + " and sender=" + Nsenderid;

            cnd.ExecuteNonQuery();

        }
        string Fnme, Snme, Prop;
        public string post(string X_loginid, string Status)
        {
            int N_loginid = Convert.ToInt32(X_loginid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            //  cnd.CommandText = "insert into newsfeed (logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus)values('" + N_loginid + "','" + Fnme + "','" + Snme + "','" + Prop + "')";
            cnd.CommandText = "select  Firstname,Surname,Profilepic from loginuser where LogginId=" + N_loginid;
            SqlDataReader rdr = cnd.ExecuteReader();
            while (rdr.Read())
            {
                Fnme = rdr["Firstname"].ToString();
                Snme = rdr["Surname"].ToString();
                Prop = rdr["Profilepic"].ToString();
                //  Stus = rdr["NFstatus"].ToString();
            }
            rdr.Close();
            cnd.CommandText = "insert into newsfeed (logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus)values('" + N_loginid + "','" + Fnme + "','" + Snme + "','" + Prop + "','" + Status + "' )";
            // con.Open();
            cnd.ExecuteNonQuery();
            con.Close();
            con.Open();
            cnd = con.CreateCommand();
            cnd.CommandText = "select logginId, NFFirstname,NFSurname, NFProfilepic,NFstatus from newsfeed  where logginId=" + N_loginid + " order by postid desc";
            // rdr.Close();
            rdr = cnd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;


        }
        public string posting(string X_loginid, string postingimage, string postingheader)
        {
            int N_loginid = Convert.ToInt32(X_loginid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            //  cnd.CommandText = "insert into newsfeed (logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus,imageposting,headerposting)values('" + N_loginid + "','" + Fnme + "','" + Snme + "','" + Prop + "','"+postingimage+"','"+postingheader+"')";
            cnd.CommandText = "select  Firstname,Surname,Profilepic from loginuser where LogginId=" + N_loginid;
            SqlDataReader rdr = cnd.ExecuteReader();
            while (rdr.Read())
            {
                Fnme = rdr["Firstname"].ToString();
                Snme = rdr["Surname"].ToString();
                Prop = rdr["Profilepic"].ToString();
                // Stus = rdr["NFstatus"].ToString();
            }
            rdr.Close();
            cnd.CommandText = "insert into  newsfeed (logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus,imgposting,headerposting)values('" + N_loginid + "','" + Fnme + "','" + Snme + "','" + Prop + "','" + "" + "','" + postingimage + "','" + postingheader + "')";
            //  cnd.CommandText = "insert into newsfeed (logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus)values('" + N_loginid + "' )";
            // con.Open();
            cnd.ExecuteNonQuery();
            con.Close();
            con.Open();
            cnd = con.CreateCommand();
            //cnd.CommandText = "insert into newsfeed (logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus)values('" + N_loginid + "' )";
            cnd.CommandText = "select postid, logginId,NFFirstname,NFSurname,NFProfilepic,NFstatus,imgposting,headerposting from  newsfeed  where logginId=" + N_loginid + " order by postid desc";
            // rdr.Close();
            rdr = cnd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;


        }

        public string save(string X_Loginid)
        {



            int N_Loginid = Convert.ToInt32(X_Loginid);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "select LogginId,Firstname,Surname,Profilepic,Email,DATENAME(month,DT_dob)as month, DATENAME(day,DT_dob)as day, DATENAME(year,DT_dob)as Year,Gender from loginuser where LogginId=" + N_Loginid;
            //   cnd.CommandText=" SELECT DATENAME(month,DT_dob)  FROM  loginuser" ;
            SqlDataReader rdr = cnd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;

        }
        public void adverte(string X_Firstname, string X_Surname, string X_Email, string X_supPassword, DateTime DT_SUPdob, string x_Gender)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            string str;
            str = "insert into loginuser(Firstname,Surname,Email,supPassword,DT_dob,Gender,Accountstatus,role)values('" + X_Firstname + "','" + X_Surname + "','" + X_Email + "','" + X_supPassword + "','" + DT_SUPdob + "','" + x_Gender + "',0,2)";
            SqlCommand cnd = new SqlCommand(str, con);
            int res = cnd.ExecuteNonQuery();
            cnd.Dispose();
            con.Close();
            if (res > 0)
            {
                sendmail();
            }




        }
        public string user_serch()
        
        {
            //int N_id=Convert.ToInt32(X_id);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select LogginId, Firstname,Surname,Profilepic from loginuser where LogginId!=81";
            SqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;


        }
        // string F_nme, S_nme, E_mail,pic;
        public void delete_user(string X_loginId, string email)
        {
            int N_id = Convert.ToInt32(X_loginId);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();


            cmd.CommandText = "insert into Bang_user(Login_id,email)values('" + N_id + "','" + email + "')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from loginuser where LogginId=" + N_id;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from frndconform where N_logginid= " + N_id;
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from post where loginid=" + N_id;

        }
        string mail;
        public bool block(string X_Email)
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select email from Bang_user where email ='" + X_Email + "'";
            SqlDataReader sdr = cmd.ExecuteReader();
            int count = 0;
            while (sdr.Read())
            {
                // mail = sdr["email"].ToString();
                count++;


            }


            if (count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

            con.Close();



        }
        public string Edit_p(string X_id)
        {
            int N_id = Convert.ToInt32(X_id);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select LogginId,Firstname,Surname,Profilepic,Email,DATENAME(month,DT_dob)as month, DATENAME(day,DT_dob)as day, DATENAME(year,DT_dob)as Year,Gender from loginuser where LogginId=81 ";
            SqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;
        }
        public string advert_log(string X_id)
        {
            int N_ID = Convert.ToInt32(X_id);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select LogginId,Firstname,Surname,Profilepic from loginuser where LogginId=" + N_ID;
            SqlDataReader rdr = cmd.ExecuteReader();
            while (rdr.Read())
            {
                Fnme = rdr["Firstname"].ToString();
                Snme = rdr["Surname"].ToString();
                Prop = rdr["Profilepic"].ToString();

            }
            rdr.Close();

            cmd.CommandText = "insert into adverting(A_loginid,adverter_status,A_fname,A_Surname,A_propic)values('" + N_ID + "',0,'" + Fnme + "','" + Snme + "','" + Prop + "')";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "select adverter_status from adverting ";
            // SqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;

        }
        public string approvel_pending(string ID)
        {
            // int n_id = Convert.ToInt32(ID);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select A_loginid,adverter_status,A_fname,A_Surname,A_propic from adverting where adverter_status=0 ";
            SqlDataReader rdr = cmd.ExecuteReader();

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;

        }
        public void suspentdate(string loginid)
        {
            int N_id = Convert.ToInt32(loginid);
            //DateTime.Now.ToString("yyyy-MM-dd");
            DateTime D = DateTime.Today;
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = "insert into suspending(Login_id,date_spnd)values('" + N_id + "','" + D + "')";
            cmd.ExecuteNonQuery();

        }
        //  string date_dlt;
        public string suspnding(string X_id)
        {

            // int n_id = Convert.ToInt32(ID);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select Login_id,date_spnd from suspending  ";
            SqlDataReader rdr = cmd.ExecuteReader();

            //DateTime dt = Convert.ToDateTime(date_dlt);

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;

        }
        public void logout(string X_loginid, out string temp, out string Stus)
        {

            temp = "";
            Stus = "";
            //  X_loginid = "";
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = cmd.CommandText = "select A_loginid, adverter_status from adverting where A_loginid=" + X_loginid;
            SqlDataReader rdr = cmd.ExecuteReader();


            while (rdr.Read())
            {
                Stus = rdr["adverter_status"].ToString();

            }
        }
        public void status_one(string X_Loginid)
        {
            int N_loginid = Convert.ToInt32(X_Loginid);

            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "update adverting set adverter_status=1 where A_loginid=" + N_loginid;
            cnd.ExecuteNonQuery();
            cnd.CommandText = "update loginuser set role=2 where LogginId =" + N_loginid;
            cnd.ExecuteNonQuery();
            
        }
        public void pic(out string tempvar ,out string image)
        {
            tempvar = "";
            image = "";
           
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cnd = con.CreateCommand();
            cnd.CommandText = "select * from flpkrt_onlie ";
           SqlDataReader rdr = cnd.ExecuteReader();


            while (rdr.Read())
            {
               image = rdr["imges"].ToString();

            }
        }
     public  string fk_content()
        {
         
          SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select id,imges from flpkrt_onlie ";
            SqlDataReader rdr = cmd.ExecuteReader();

            //DateTime dt = Convert.ToDateTime(date_dlt);

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;

        }
        public string user_products()
     {
         SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
         con.Open();
         SqlCommand cmd = con.CreateCommand();
         cmd.CommandText = "select *from product_tble";
         SqlDataReader rdr = cmd.ExecuteReader();

         //DateTime dt = Convert.ToDateTime(date_dlt);

         DataTable dt = new DataTable();
         dt.Load(rdr);
         string str = datatabletojson(dt);
         return str;


     }
        public string cstomr_product()
        {
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select *from buy_table";
            SqlDataReader rdr = cmd.ExecuteReader();

            //DateTime dt = Convert.ToDateTime(date_dlt);

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;


        }
        public string details_view( string X_id)
        {
            int N_id = Convert.ToInt32(X_id);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from buy_table where id =" + N_id;
            SqlDataReader rdr = cmd.ExecuteReader();

            //DateTime dt = Convert.ToDateTime(date_dlt);

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;


        }
        public string product_buying()
        {
           // int N_id = Convert.ToInt32(X_id);
            SqlConnection con = new SqlConnection("Data Source=DESKTOP-46KB8VH\\SQLEXPRESS;Initial Catalog=facebooklogin;Integrated Security=True");
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandText = "select * from order_details";
            SqlDataReader rdr = cmd.ExecuteReader();

            //DateTime dt = Convert.ToDateTime(date_dlt);

            DataTable dt = new DataTable();
            dt.Load(rdr);
            string str = datatabletojson(dt);
            return str;


        }
        }
      









    }

       
        


    


