using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ForestMarathone.Views.Sponsor
{
    /// <summary>
    /// Логика взаимодействия для SponsorRunner.xaml
    /// </summary>
    public partial class SponsorRunner : Window
    {

        Sponsors sponsor;

        public SponsorRunner(Sponsors sponsor)
        {
            InitializeComponent();

            this.sponsor = sponsor;

            using(var context = new ForestMarathoneEntities())
            {
                _runners.ItemsSource = context.Participant
                    .Where(x => x.Users.UserStatusId == UStatus.Activated)
                    .Select(x => 
                    new { 
                        Runner = x.LastName + " " + x.FirstName + " - " + x.Countries.Title,
                        Id = x.Id
                    })
                    .ToList();

                _runners.DisplayMemberPath = "Runner";
                _runners.SelectedValuePath = "Id";

                if (_runners.Items.Count > 0)
                    _runners.SelectedIndex = 0;
            }


            _sponsorMoney.Text = "0";
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Lower(object sender, RoutedEventArgs e)
        {
            if (Convert.ToInt32(_sponsorMoney.Text) - 100 < 0)
                return;

            _sponsorMoney.Text = (Convert.ToInt32(_sponsorMoney.Text) - 100).ToString();
        }

        private void Rise(object sender, RoutedEventArgs e)
        {
            _sponsorMoney.Text = (Convert.ToInt32(_sponsorMoney.Text) + 100).ToString();
        }

        private void Sponsor(object sender, RoutedEventArgs e)
        {
            if (_cvc.Text.Length != 3 || String.IsNullOrWhiteSpace(_cvc.Text))
            {
                MessageBox.Show("Не введён код безопасности банковской карты!");
                return;
            }

            var sponsoredParticipant = new SponsoredParticipants()
            {
                ParticipantId = (int)_runners.SelectedValue,
                SponsorId = sponsor.Id,
                SponsoredMoney = Convert.ToDecimal(_sponsorMoney.Text)
            };

            using (var context = new ForestMarathoneEntities())
            {

                context.SponsoredParticipants.Add(sponsoredParticipant);

                var participantToUpdate = context.Participant.Where(x => x.Id == sponsoredParticipant.ParticipantId).FirstOrDefault();
                participantToUpdate.Wallet += participantToUpdate.Wallet + sponsoredParticipant.SponsoredMoney;
                var sponsorToUpdate = context.Sponsors.Where(x => x.Id == sponsor.Id).FirstOrDefault();
                sponsorToUpdate.TotalSum += Convert.ToDecimal(_sponsorMoney.Text);
                context.SaveChanges();
            }

            MessageBox.Show("Вы успешно профинансировали бегуна '" + _runners.SelectedItem + "'");
        }
    }
}
