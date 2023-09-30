using AD_manager.Models;
using AD_manager.Services;
using Microsoft.Maui.Controls;
using System.Linq;

namespace AD_manager.Views;

public partial class AddNewOrg : ContentPage
{
	DataBaseManager db = new DataBaseManager();
	List<string> wilays = new List<string>();
	public event EventHandler ModelClosed;
    List<Organization> listOfOrganizations = new List<Organization>();

    public AddNewOrg(  List<Organization> listOfOrganizations )
	{
		InitializeComponent();
		List<Organization> listOfOrganization = db.GetAllOrganization();
		this.listOfOrganizations = listOfOrganization;

		wilays = new List<string>() {
            "Illizi","Ouargla","Adrar", "Chlef", "Laghouat", "Oum El Bouaghi", "Batna", "Bejaia", "Biskra", "Bechar", "Blida",
            "Bouira", "Tamanrasset", "Tebessa", "Tlemcen", "Tiaret", "Tizi Ouzou", "Alger", "Djelfa",
            "Jijel", "Setif", "Saida", "Skikda", "Sidi Bel Abbes", "Annaba", "Guelma", "Constantine",
            "Medea", "Mostaganem", "MSila", "Mascara",  "Oran", "El Bayadh", "Bordj Bou Arreridj",
            "Boumerdes", "El Tarf", "Tindouf", "Tissemsilt", "El Oued", "Khenchela", "Souk Ahras", "Tipaza",
            "Mila", "Ain Defla", "Naama", "Ain Témouchent", "Ghardaia", "Relizane"
        };

        entry_wilay.Focused += (sender, e) =>
        {
            list_wilaya.ItemsSource = wilays;
        };

        entry_wilay.Unfocused += (sender, e) =>
        {
            list_wilaya.ItemsSource = null;
        };
    }


	private void Entry_wilay_TextChanged(object sender, TextChangedEventArgs e)
	{
		var filtredWilays = wilays.Where(s => s.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
		list_wilaya.ItemsSource = filtredWilays;

    }
    private void list_wilaya_tapped(object sender, EventArgs e)
	{
		var tapped = (TextCell)sender;
		entry_wilay.Text = tapped.Text;
		list_wilaya.ItemsSource = null;

		
    }

	private async void submit_clicked(object sender, EventArgs e)
	{
		string ent_org = entry_org.Text;
		string ent_wilaya = entry_wilay.Text;
		string ent_baladia = entry_baladia.Text;
		string ent_address = entry_address.Text;
		int result = 0;

		
		


		if (!string.IsNullOrEmpty(ent_org) && !string.IsNullOrEmpty(ent_wilaya) && !string.IsNullOrEmpty(ent_baladia))
		{

            var searchedText = listOfOrganizations.Where(s => s.OrganizationName.Contains(ent_org, StringComparison.OrdinalIgnoreCase));
			if (searchedText.Count() > 0)
			{
				await DisplayAlert("تنبيه", "اسم هده المؤسسة موجود من قبل", "حسنا");
			}
			else
			{

				try
				{
					result = db.AdddNewOrganization(ent_org, ent_wilaya, ent_baladia, ent_address);
					if (result != 0)
					{
						await DisplayAlert(" ", "تم اضافة العنصر", "حسنا");
                        entry_address.Text = null;
                        entry_org.Text = null;
                        
                    }
					else
					{
						await DisplayAlert("تنبيه", "لم يتم اضافة العنصر", "حسنا");
					}

				}
				catch (Exception ex)
				{
					Console.WriteLine("faild to add new organization page AddNewOrg {0}", ex.Message);
					await DisplayAlert("تنبيه", "لم يتم اضافة العنصر", "حسنا");
					entry_address.Text = null;
					entry_baladia.Text = null;
					entry_org.Text = null;
					entry_wilay.Text = null;
				}
			}
		}
		else
		{
			await DisplayAlert("تنبيه", "اسم المؤسسة و الولاية و البلدية اجباري", "حسنا");
		}
		

	}

	private async void cancel_clicked(object sender, EventArgs e)
	{
		await Navigation.PopModalAsync();
		ModelClosed?.Invoke(this,EventArgs.Empty);
    }
}