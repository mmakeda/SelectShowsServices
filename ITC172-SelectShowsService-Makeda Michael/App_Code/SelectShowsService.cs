using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

// NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "SelectShowsService" in code, svc and config file together.
public class SelectShowsService : ISelectShowsService
{
    ShowTrackerEntities st = new ShowTrackerEntities();

    public List<string> GetArtists()
    {
        var arts = from a in st.Artists orderby a.ArtistName select new { a.ArtistName };

        List<string> artists = new List<string>();

        foreach (var ar in arts)
        {

            artists.Add(ar.ArtistName.ToString());
        }

        return artists;

    }

    public List<string> GetShows()
    {
        var shw = from s in st.Shows orderby s.ShowName select new { s.ShowName };

        List<string> showlist = new List<string>();

        foreach (var sh in shw)
        {

            showlist.Add(sh.ShowName.ToString());
        }

        return showlist;

    }

    public List<string> GetVenues()
    {
        var ve = from v in st.Venues orderby v.VenueName select new { v.VenueName };

        List<string> venuelist = new List<string>();

        foreach (var vs in ve)
        {

            venuelist.Add(vs.VenueName.ToString());
        }

        return venuelist;

    }

    public List<ShowLite> GetShowByArtist(string artistname)
    {
        var sba = from s in st.Shows
                  from sb in s.ShowDetails
                  where sb.Artist.ArtistName.Equals(artistname)
                  select new { s.Venue.VenueName, s.ShowName, s.ShowDate };

        List<ShowLite> sbalist = new List<ShowLite>();

        foreach (var s in sba)
        {
            ShowLite slite = new ShowLite();
            slite.ShowName = s.ShowName;
            slite.ShowDate = s.ShowDate.ToShortDateString();
         
            slite.VenueName = s.VenueName;

            sbalist.Add(slite);
        }

        return sbalist;


    }

 public List<ShowLite> GetShowByVenue(string venuename)
    {
        var sbv = from s in st.Shows
                  where s.Venue.VenueName.Equals(venuename)
                  select new { s.Venue.VenueName, s.ShowName, s.ShowDate, s.ShowTime };

        List<ShowLite> sbvlist = new List<ShowLite>();

        foreach (var s in sbv)
        {
            ShowLite vlite = new ShowLite();
            vlite.ShowName = s.ShowName;
            vlite.ShowDate = s.ShowDate.ToShortDateString();
            vlite.ShowTime = s.ShowTime.ToString();
            vlite.VenueName = s.VenueName;
            sbvlist.Add(vlite);
        }

        return sbvlist;


    } 
}

