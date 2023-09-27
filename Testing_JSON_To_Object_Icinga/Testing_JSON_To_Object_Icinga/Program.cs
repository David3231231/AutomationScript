using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;

class Program
{
    static async Task Main()
    {
        string url = "https://icinga.onair.broadcastservices.cloud/icingaweb2/monitoring/list/hosts?servicegroup_name=ABC%20Classic%20TX%20Servicegroup&modifyFilter=1";
        string username = "abc_dataminer";
        string password = "RJh.A84dy6n_9GT5zu";

        using (HttpClient client = new HttpClient())
        {
            // Set basic authentication header
            string authHeaderValue = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes($"{username}:{password}"));
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);

            // Set Accept header to application/json
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                string jsonContent = await response.Content.ReadAsStringAsync();
                
                List<Class1> hosts = JsonConvert.DeserializeObject<List<Class1>>(jsonContent);

               // Class1 hosts = JsonConvert.DeserializeObject<Class1>(jsonContent);

                foreach (Class1 host in hosts)
                {
                     Console.WriteLine("Host Name: " + host.host_display_name);    
                }

                //// Access the deserialized objects
                //foreach (Rootobject host in hosts)
                //{
                //Console.WriteLine("Host Name: " + host.Property1[0].host_name);
                //Console.WriteLine("Host Display Name: " + host.Property1[0].host_display_name);
                ////Console.WriteLine("Host State: " + host.host_state);
                ////Console.WriteLine("Host Output: " + host.host_output);
                ////Console.WriteLine("Host Attempt: " + host.host_attempt);
                ////Console.WriteLine("Host Last State Change: " + host.host_last_state_change);
                ////Console.WriteLine("Host Next Update: " + host.host_next_update);
                ////Console.WriteLine("---------------------------------------");
                // }

            }
            else
            {
                Console.WriteLine($"API request failed with status code: {response.StatusCode}");
            }
        }
    }
}
public class Class1
{
    public string host_icon_image { get; set; }
    public string host_icon_image_alt { get; set; }
    public string host_name { get; set; }
    public string host_display_name { get; set; }
    public string host_state { get; set; }
    public string host_acknowledged { get; set; }
    public string host_output { get; set; }
    public string host_attempt { get; set; }
    public string host_in_downtime { get; set; }
    public string host_is_flapping { get; set; }
    public string host_state_type { get; set; }
    public string host_handled { get; set; }
    public string host_last_state_change { get; set; }
    public string host_notifications_enabled { get; set; }
    public string host_active_checks_enabled { get; set; }
    public string host_passive_checks_enabled { get; set; }
    public string host_check_command { get; set; }
    public string host_next_update { get; set; }
}

