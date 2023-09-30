using AD_manager.Models;
using AD_manager.Services;

namespace AD_manager.Views;

public partial class Delivary_Page1 : ContentPage
{
    DataBaseManager db = new DataBaseManager();
    List<Organization> listOfOrganization = new List<Organization>();
    List<Sector> listOfSectors = new List<Sector>();
    
    public Delivary_Page1()
	{
		InitializeComponent();
        listOfOrganization = db.GetAllOrganization();
        listOfSectors = db.GetAllSectors();
        entry_orgName.Focused += (sender,e)=>
        {
            list_orgName.ItemsSource = listOfOrganization;
        };
        entry_orgName.Unfocused += (sender, e) =>
        {
            list_orgName.ItemsSource = null;
        };
        entry_sectorName.Focused += (sender, e) =>
        {
            list_sectorName.ItemsSource = listOfSectors;
        };
        entry_sectorName.Unfocused += (sender, e) =>
        {
            list_sectorName.ItemsSource = null;
        };
    }


    private void entry_sectorName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filtredSectors = listOfSectors.Where(s=>s.SectorName.Contains(e.NewTextValue,StringComparison.OrdinalIgnoreCase));
        list_sectorName.ItemsSource = filtredSectors;
    }

    private void entry_orgName_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filtredOrganization = listOfOrganization.Where(s => s.OrganizationName.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
        list_orgName.ItemsSource = filtredOrganization;
    }

    private void onTappedlist_org(object sender, EventArgs e)
    {
        var tapped = (TextCell)sender;
        entry_orgName.Text = tapped.Text;
        list_orgName.ItemsSource = null;
    }
    private void onTappedList_sector(object sender, EventArgs e)
    {
        var tapped = (TextCell)sender;
        entry_sectorName.Text = tapped.Text;
        list_sectorName.ItemsSource = null;
    }

    private async void OnClick_AddNewOrg(object sender, EventArgs e)
    {
        var modal = new AddNewOrg(listOfOrganization);
        modal.ModelClosed += (e, args) =>
        {
            listOfOrganization = db.GetAllOrganization();
        };
        await Navigation.PushModalAsync(modal);
    }

    private async void OnClick_AddNewSector(object sender, EventArgs e)
    {
        string input_sectorName = await DisplayPromptAsync("اسم القطاع", "اسم القطاع",accept:"اضافة",cancel:"الغاء");
        
        if (!string.IsNullOrEmpty(input_sectorName))
        {
            var searchedText = listOfSectors.Where(s => s.SectorName.Contains(input_sectorName, StringComparison.OrdinalIgnoreCase));
            
            while (!string.IsNullOrEmpty(input_sectorName) && searchedText.Any())
            {
                
                
                await DisplayAlert("تنبيه", "اسم القطاع موجود من قبل", "حسنا");

                input_sectorName = await DisplayPromptAsync("اسم القطاع", "اسم القطاع", accept: "اضافة", cancel: "الغاء");

                searchedText = listOfSectors.Where(s => s.SectorName.Contains(input_sectorName, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(input_sectorName))
            {
                db.AddNewSector(input_sectorName);
                await DisplayAlert("تنبيه", "تم اضافة القطاع", "حسنا");
                listOfSectors = db.GetAllSectors();
            }
        }
        
        
        
            
       
        
        
    }

}