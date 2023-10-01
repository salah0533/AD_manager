using AD_manager.Models;
using AD_manager.Services;

namespace AD_manager.Views;

public partial class Delivar_Page2 : ContentPage
{
    int currentYear;
    string order_id;
    
    DataBaseManager db = new DataBaseManager();
    List<Product> listOfProduct = new List<Product>();
    Delevary_product lastDelevaryProduct;
    int currecnt_product_id ;
    int excuteOnes = 0;
    public Delivar_Page2(int Organization_id)
	{
		InitializeComponent();
        this.currentYear = DateTime.Now.Year;
        int N_Orders = db.GetNumberOfOrders(DateTime.Now.Year);
        //should add handler her if the n_oders is -1
        this.order_id = currentYear.ToString() + "/" + N_Orders.ToString();
      
            
        this.listOfProduct = db.GetAllProduct();
        Order_ID.Text = order_id;

        list_productName.Focused += (sender, e) =>
        {
            list_productName.ItemsSource = listOfProduct;
        };

        list_productName.Unfocused += (sender, e) =>
        {
            list_productName.ItemsSource = null;
        };

        entry_achat.Focused += (sender, e) =>
        {
            if(excuteOnes == 0)
            {


                /*lastDelevaryProduct = db.GetTheLastDelivaryProductByOrganization(Organization_id,);
                entry_achat.Text = lastDelevaryProduct.PrixAchat.ToString();
                entry_vont.Text = lastDelevaryProduct.prixVont.ToString();*/
            }
        };


    }

    /*private void list_product_tapped(object sender, ItemTappedEventArgs e)
    {
        int index = e.ItemIndex;
        this.currecnt_product_id =  listOfProduct[index].Id;
        entry_productName.Text = listOfProduct[index].Name;
    }*/

    private void enry_productName_textChange(object sender, TextChangedEventArgs e)
    {
        var filtredProductNames = listOfProduct.Where(s=>s.Name.Contains(e.NewTextValue,StringComparison.OrdinalIgnoreCase));
        list_productName.ItemsSource = filtredProductNames;
    }


}