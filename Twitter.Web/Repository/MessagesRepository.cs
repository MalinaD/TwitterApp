namespace Twitter.Data.Repositories
{

    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Twitter.Models;
    using Twitter.Web.Hubs;

    public class MessagesRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Notification> GetAllMessages()
        {
            var messages = new List<Notification>();
            using (var connection = new SqlConnection(_connString))
            {
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [Id], [Content], [DateModified] FROM [dbo].[Notification]", connection))
                {
                    command.Notification = null;

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Notification 
                        { 
                            Id = (int)reader["Id"],
                            Content = (string)reader["Content"],
                            //EmptyMessage = reader["EmptyMessage"] != DBNull.Value ? (string)reader["EmptyMessage"] : "",
                            DateModified = Convert.ToDateTime(reader["DateModified"])
                        });
                    }
                }

            }
            return messages;


        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                NotificationsHub.SendMessages();
            }
        }
    }
}
