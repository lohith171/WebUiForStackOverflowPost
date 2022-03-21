using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUiForStackOverflowPost.Models
{
    public class StackOverflowPost
    {
        public string title { get; set; }
        public string description { get; set; }
        DateTime dateTime { get; set; }

        public int vote;
        public StackOverflowPost()
        {

        }
        public string getTitle()
        {
            return title;
        }
        public StackOverflowPost(string title, string description)
        {
            this.title = title;
            this.description = description;
            vote = 0;
            dateTime = DateTime.Now;
        }

        public void UpVote()
        {
            vote++;
        }
        public void DownVote()
        {
            vote--;
        }
        public int getVoteValue() { return vote; }
    }
}