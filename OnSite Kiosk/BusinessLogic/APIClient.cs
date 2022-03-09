using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace OnSite_Kiosk.BusinessLogic
{
    class APIClient
    {
        private static readonly HttpClient client = new HttpClient();
        private static Windows.Storage.ApplicationDataContainer localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;

        private String _APIBase = "";

        public APIClient(){
            object APIBase;

            if (localSettings.Values.TryGetValue("APIBase", out APIBase))
            {
                _APIBase = APIBase.ToString();
            }
        }

        // General
        async public Task<Person> GetUserByBarcode(String barcode)
        {

            var content = new FormUrlEncodedContent(new Dictionary<String, String> { { "barcode", barcode } });
            try
            {
                var response = await client.PostAsync(_APIBase + "/GetUserByBarcode", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // success
                    Person person = JsonConvert.DeserializeObject<Person>(responseString);
                    if (!String.IsNullOrEmpty(person.ID))
                    {
                        return person;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        // Staff
        async public Task<List<Person>> StaffSearch(String search)
        {

            var content = new FormUrlEncodedContent(new Dictionary<String, String> { { "search", search } });
            try
            {
                var response = await client.PostAsync(_APIBase + "/StaffSearch", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // success
                    List<Person> people = JsonConvert.DeserializeObject<List<Person>>(responseString);
                    return people;
                }
            }
            catch (Exception e)
            {
                throw e; 
            }
            return null;
        }

        async public Task<bool> StaffSignIn(Person person, String siteid, String tag = null)
        {
            var dict = new Dictionary<String, String>{
                { "id", person.ID.ToString() },
                { "siteid", siteid }
            };

            if (tag != null)
            {
                dict.Add("tag", tag);
            }

            var content = new FormUrlEncodedContent( dict);


            try
            {
                var response = await client.PostAsync(_APIBase + "/StaffSignIn", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode == true && responseString == "true";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        async public Task<bool> StaffSignOut(Person person, String siteid, SignOutReason reason, DateTime? expectereturn = null, String comment = null, String tag = null)
        {
            Dictionary<String, String> dict = new Dictionary<String, String> {
                { "id", person.ID.ToString() },
                { "siteid", siteid },
                { "reason", reason.ReasonID.ToString() },

            };

            if (expectereturn.HasValue)
            {
                dict.Add("return", expectereturn.Value.ToString("yyyy-MM-dd HH:mm:00"));
            }
            if (comment != null)
            {
                dict.Add("comment", comment);
            }
            if (tag != null)
            {
                dict.Add("tag", tag);
            }
            var content = new FormUrlEncodedContent(dict);

            try
            {
                var response = await client.PostAsync(_APIBase + "/StaffSignOut", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode == true && responseString == "true";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Students

        async public Task<List<String>> StudentActions(Person person)
        {
            var content = new FormUrlEncodedContent(new Dictionary<String, String> { { "id", person.ID } });
            try
            {
                var response = await client.PostAsync(_APIBase + "/StudentActions", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // success
                    List<String> actions = JsonConvert.DeserializeObject<List<String>>(responseString);
                    return actions;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        async public Task<List<Person>> StudentSearch(String search)
        {

            var content = new FormUrlEncodedContent(new Dictionary<String, String> { { "search", search } });
            try
            {
                var response = await client.PostAsync(_APIBase + "/StudentSearch", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // success
                    List<Person> people = JsonConvert.DeserializeObject<List<Person>>(responseString);
                    return people;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
        
        //TODO: ParentSMS
        async public Task<bool> StudentSignOut(Person person, String siteid, SignOutReason reason, String parentmobile DateTime? expectereturn = null)
        {
            Dictionary<String, String> dict = new Dictionary<String, String> {
                { "id", person.ID.ToString() },
                { "reason", reason.ReasonID.ToString() },
                {"siteid", siteid },
                {"reasontext", reason.Description.ToString() },
            };

            if (expectereturn.HasValue)
            {
                dict.Add("return", expectereturn.Value.ToString("yyyy-MM-dd HH:mm:00"));
            }
                var content = new FormUrlEncodedContent(dict);
                

            try
            {
                var response = await client.PostAsync(_APIBase + "/StudentSignOut", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode == true && responseString == "true";
            }
            catch (Exception e)
            {

                throw e;
            }
        
            if (respone.IsSuccessStatusCode == true)
            {
                
            }

        }

        // async public Task<bool> ParentSMS(Person person )
        // {
        // Dictionary<String, String> dict = new Dictionary<String, String> {
        //         { "id", person.ID.ToString() },
        //         { "reason", reason.ReasonID.ToString() },
        //         {"siteid", siteid },
        //         {"reasontext", reason.Description.ToString() }
        //     };
        
        // {
        //     var content = new FormUrlEncodedContent(new Dictionary<String, String> { { "sms", search } });
        //     try
        //     {
        //         var response = await client.PostAsync(_APIBase + "/sms", content);
        //         var responseString = await response.Content.ReadAsStringAsync();

        // }

        async public Task<bool> StudentLate(Person person, StudentLateReason reason)
        {
            Dictionary<String, String> dict = new Dictionary<String, String> {
                { "id", person.ID.ToString() },
                { "reason", reason.ID.ToString() }

            };

            var content = new FormUrlEncodedContent(dict);

            try
            {
                var response = await client.PostAsync(_APIBase + "/StudentLate", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode == true && responseString == "true";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        async public Task<bool> StudentSignIn(Person person)
        {
            Dictionary<String, String> dict = new Dictionary<String, String> {
                { "id", person.ID.ToString() }

            };

            var content = new FormUrlEncodedContent(dict);

            try
            {
                var response = await client.PostAsync(_APIBase + "/StudentSignIn", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode == true && responseString == "true";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        // Guests
        async public Task<Person> GuestSearch(String firstname, String lastname, String mobile)
        {

            var content = new FormUrlEncodedContent(new Dictionary<String, String> { 
                { "firstname", firstname },
                { "lastname", lastname },
                { "mobile", mobile }
            });
            try
            {
                var response = await client.PostAsync(_APIBase + "/GuestSearch", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // success
                    if (responseString == "[]")
                    {
                        return null;
                    }
                    Person person = JsonConvert.DeserializeObject<Person>(responseString);
                    if (!String.IsNullOrEmpty(person.ID))
                    {
                        return person;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        async public Task<List<GuestPass>> GuestGetSignedIn(String siteid)
        {

            var content = new FormUrlEncodedContent(new Dictionary<String, String> {
                { "siteid", siteid }
            });
            try
            {
                var response = await client.PostAsync(_APIBase + "/GuestGetSignedIn", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    // success
                    List<GuestPass> signedIn = JsonConvert.DeserializeObject<List<GuestPass>>(responseString);
                    return signedIn;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        async public Task<GuestPass> GuestGetSignIn(String guid)
        {

            var content = new FormUrlEncodedContent(new Dictionary<String, String> {
                { "guid", guid }
            });
            try
            {
                var response = await client.PostAsync(_APIBase + "/GuestGetSignIn", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK && responseString != "false")
                {
                    // success
                    GuestPass signedIn = JsonConvert.DeserializeObject<GuestPass>(responseString);
                    return signedIn;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        async public Task<GuestPass> GuestSignIn(String siteid, String firstname, String lastname, String mobile, String company = "", String wwvp = null, String wwvpverifiedby = null, Person staffcontact = null, bool internet = false)
        {

            var dict = new  Dictionary<String, String> {
                { "siteid", siteid },
                { "firstname", firstname },
                { "lastname", lastname },
                { "mobile", mobile },
                { "company", company },
                { "internet", internet.ToString() }
            };
            if (wwvp != null)
            {
                dict.Add("wwvp", wwvp);
            }

            if (staffcontact != null)
            {
                dict.Add("staffcontact", staffcontact.ID);
            }
            if (wwvpverifiedby != null)
            {
                dict.Add("wwvpverifiedby", wwvpverifiedby);
            }

            var content = new FormUrlEncodedContent(dict);
            try
            {
                var response = await client.PostAsync(_APIBase + "/GuestSignIn", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (responseString == "false")
                        return null;
                    // success
                    GuestPass guestpass = JsonConvert.DeserializeObject<GuestPass>(responseString);
                    return guestpass;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }

        async public Task<bool> GuestSignOut(String siteid, GuestPass pass)
        {
            var content = new FormUrlEncodedContent(new Dictionary<String, String> {
                { "guid", pass.GUID.ToString() },
                { "siteid", siteid }
            });
            try
            {
                var response = await client.PostAsync(_APIBase + "/GuestSignOut", content);
                var responseString = await response.Content.ReadAsStringAsync();

                return response.IsSuccessStatusCode == true && responseString == "{\"result\":1}";
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        async public Task<WWVP> VerifyWWVP(String lastname, String wwvp)
        {
            var content = new FormUrlEncodedContent(new Dictionary<String, String> {
                { "lastname", lastname },
                { "wwvp", wwvp }
            });
            try
            {
                var response = await client.PostAsync(_APIBase + "/WWVPVerify", content);
                var responseString = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (responseString == "false")
                    {
                        return null;
                    }
                    // success
                    WWVP wwvpo = JsonConvert.DeserializeObject<WWVP>(responseString);
                    if (!String.IsNullOrEmpty(wwvpo.RegistrationNumber))
                    {
                        return wwvpo;
                    }
                    return null;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return null;
        }
    }
}
