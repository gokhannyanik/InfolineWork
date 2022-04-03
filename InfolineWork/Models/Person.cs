using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfolineWork.Models
{
    public class ImageP
    {
        public byte[] binaryImage { get; set; }
    }

    public class PersonModel
    {
        public int PersonId { get; set; }
        public string AdSoyad { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string PersonPhoto { get; set; }
        public byte[] binaryImage { get; set; }

    }
    public class PersonQuestion
    {
        public int PersonId { get; set; }
        public string Name { get; set; }
        public string PersonPhoto { get; set; }
        public int QuestionId { get; set; }
        public string Question { get; set; }
    }

    public class PersonAnswer
    {
        public string Name { get; set; }
        public string Photo { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public DateTime? AnswerDate { get; set; }
    }



}