using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace fbfinal.Models
{
    public class ModelClass1
    {
         public string name="";
        public string pwd="";
        public string X_emailaddress
    {
        get
        {
            return name;
        }
            set
            {
                name=value;
            }
    }
        public string x_password
        {
            get
            {
                return pwd;
            }
            set
            {
                pwd = value;
            }
        }
        String msg;
        public string login()
        {
            
            try
            {
                ServiceReference1.Service1Client ser1 = new ServiceReference1.Service1Client();
                msg = ser1.Login(X_emailaddress, x_password);
              // return msg;

            }
        

            catch (Exception ec)
            {
           //    errorlogin.SendError(ec);
            }

            return msg;
        } 
     
        
            public string fnme="";
            public string snme="";
            public string emil="";
            public string psw="";
            public string pop="";
            public string dy="";
            public string mth="";
            public string yer="";
            public string gdr="";
            public string Login;
            private string DT_SUPdob;
        

       public string X_Firstname
       {
           get
           {
               return fnme;
           }
           set
           {
               fnme=value;
           }
       }
            public string X_Surname
            {
                get
                {
                    return snme;
                }
                set
                {
                    snme=value;
                }
            }
            public string X_Email
            {
                get
                {
                    return emil;
                }
                set
                {
                    emil = value;

                }
            }
            
           
            public string X_propic
            {
                get
                {
                    return pop;
                }
                set
                {
                    pop = value;
                }
            }
            
            

            public string X_supPassword
            {
                get
                {
                    return psw;
                }
                set
                {
                    psw=value;
                }
            }
            public string X_Day
            {
                get
                {
                    return dy;
                }
                set
                {
                    dy=value;
                }
            }
            public string X_Month
            {
                get
                {
                    return mth;
                }
                set
                {
                    mth=value;
                }
            }
            public string X_Year
            {
                get
                {
                    return yer;
                }
                set
                {
                    yer=value;
                }
            }
            public string X_Gender
            {
                get
                {
                    return gdr;
                }
                set
                {
                    gdr= value;
                }
            }
       
      
            
            public string Signup(string propics)

            {

                string mg="";

                ServiceReference1.Service1Client ser = new ServiceReference1.Service1Client();
                DateTime DT_dob = Convert.ToDateTime(X_Month + "/" + X_Day + "/" + X_Year,null);

                mg = ser.Signupmethod(X_Firstname, X_Surname, propics, X_Email, X_supPassword, DT_dob, X_Gender);
                return mg;
            }
           
    
        //    public string sch = "";
        //    public string srchterm
        //    {
        //        get
        //        {
        //            return sch;
        //        }
        //        set
        //        {
        //            sch = value;
        //        }
        //    }

        //public string  serch()
        //{
        //    string sh;
        //    ServiceReference1.Service1Client snt = new ServiceReference1.Service1Client();
        //    sh = snt.serchtype(srchterm);
        //    return sh;
        //}
        


               }
    public class Friends
    {
        public string X_Firstname { get; set; }
        public string X_Surname { get; set; }
        public string X_Email { get; set; }
        public string X_propic { get; set; }
    }
        
    }
