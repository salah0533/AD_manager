using AD_manager.Models;
using AD_manager.Services;

namespace AD_manager.Views;

public partial class Delivary_products : ContentPage
{
    string saller = null;
    int organizationID;
    DateTime date = new DateTime();
    string order_id_ = null;
    int sector;
    int recivedID;
    DataBaseManager db = new DataBaseManager();
    List<Product> listOfProducts = new List<Product>();
    List<Saller> listOfSallers = new List<Saller>();

    // Parameterless constructor
    public Delivary_products()
    {
        InitializeComponent();
        
    }
    public Delivary_products(int orgID=-1,string orderID=null,int receverID=-1)
	{
		InitializeComponent();

        listOfProducts = db.GetAllProduct();
        listOfSallers = db.GetAllSallers();

        entry_productName.Focused += (sender, e) =>
        {
            list_ProductName.ItemsSource = listOfProducts;
        };
        entry_productName.Unfocused += (sender, e) =>
        {
            list_ProductName.ItemsSource = null;
        };
        entry_saller.Focused += (sender, e) =>
        {
            list_sallers.ItemsSource = listOfSallers;
        };
        entry_saller.Unfocused += (sender, e) =>
        {
            list_sallers.ItemsSource = null;
        };
        entry_achatPrix.Focused += (sender, e) =>
        {
            if (!string.IsNullOrEmpty(entry_productName.Text))
            {
                var result = db.GetProductByName(entry_productName.Text);
                if(result != null)
                {
                    Delevary_product lastDelivaryProduct =  db.GetTheLastDelivaryProductByOrganization(organizationID, result.Id);
                    entry_achatPrix.Text = lastDelivaryProduct.PrixAchat.ToString();
                    entry_vontPrix.Text = lastDelivaryProduct.prixVont.ToString();
                }
            }
        };
    }

    private async void btn_AddNewProduct_Clicked(object sender, EventArgs e)
    {
        
    }

    private void TextCell_Tapped(object sender, EventArgs e)
    {
        if(sender is TextCell textCell)
        {
            entry_productName.Text = textCell.Text;
        }
    }

    private void btn_AddNewSaller_Clicked(object sender, EventArgs e)
    {

    }

    private void TextCell_Tapped_1(object sender, EventArgs e)
    {
        if(sender is TextCell textCell)
        {
            entry_saller.Text = textCell.Text;
        }
    }

    private void entry_productName_TextChanged(object sender, TextChangedEventArgs e)
    {
        
        var filtredProductNames = listOfProducts.Where(s => s.Name.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
        list_ProductName.ItemsSource = filtredProductNames;
    }

    private void entry_saller_TextChanged(object sender, TextChangedEventArgs e)
    {
        var filtredProductNames = listOfSallers.Where(s => s.sallerName.Contains(e.NewTextValue, StringComparison.OrdinalIgnoreCase));
        list_ProductName.ItemsSource = filtredProductNames;
    }

    private async void submit_Clicked(object sender, EventArgs e)
    {
        int sallerid = db.GetIdSallerByName(entry_saller.Text);
        var product_ = db.GetProductByName(entry_productName.Text);

        if(isnumiricNotEmpty(entry_achatPrix.Text) && isnumiricNotEmpty(entry_achatPrix.Text)){
            if(sallerid != -1 && product_ != null)
            {

                db.AddNewOrder(this.organizationID,recivedID,product_.sectorID,date);
            }
            else
            {
                await DisplayAlert("تنبيه", "يرجى ادراج او تصحيح اسم المنتج أو البائع", "حسنا");
            }
        }
        else
        {
            await DisplayAlert("تنبيه", "يرجى ادراج السعر", "حسنا");
        }
    }


    private bool isnumiricNotEmpty(string s)
    {
        if(string.IsNullOrEmpty(s)) return false;

        foreach(char c in s)
        {
            if(!char.IsDigit(c)) return false;
        }

        return true;
    }
}