using AD_manager.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AD_manager.Services
{
    class DataBaseManager
    {
        public DataBaseManager() {

            this._dbPath = Path.Combine(FileSystem.AppDataDirectory, "TodoSQLite.db3");
        }


        public SQLiteConnection conn;
        string _dbPath;

        public void Init()
        {
            if (File.Exists(this._dbPath))
            {
                if (this.conn != null)
                    return;
            }

            this.conn = new SQLiteConnection(_dbPath);

            this.conn.CreateTable<Organization>();
            this.conn.CreateTable<Product>();
            this.conn.CreateTable<Sector>();
            this.conn.CreateTable<Saller>();
        }

        public int AddNewProduct(string name,int SectorID)
        {
            int result = 0;
            name = name.ToLower();
            try
            {
                // enter this line
                Init();

                // basic validation to ensure a name was entered
                if (string.IsNullOrEmpty(name))
                    throw new Exception("Valid name required");

                // enter this line
                result = this.conn.Insert(new Product { Name = name ,sectorID =SectorID });

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return result;

        }
        public int AdddNewOrganization(string org_name, string org_wilaya, string org_baladia, string org_adress)
        {
            int result = 0;
            try
            {
                Init();

                result = conn.Insert(new Organization { OrganizationName = org_name.ToLower(), wilaya = org_wilaya.ToLower(), baladia = org_baladia.ToLower(), adress = org_baladia.ToLower() });
                //result = this.conn.Insert(new Organization { OrganizationName=org_name});

            }
            catch (Exception ex)
            {
                Console.WriteLine("couldn't addd organization");
            }
            return result;
        }

        public int AddNewSector(string SectorName)
        {
            int result = 0;
            try
            {
                Init();
                result = conn.Insert(new Sector { SectorName = SectorName.ToLower() });

            }
            catch (Exception e)
            {
                Console.WriteLine("problem while adding new sector");
                Console.WriteLine(e.ToString());
                return result;
            }
            return result;
        }

        public int AddNewOrder(int org_id, int sector_id, int reciver_id, DateTime date)
        {
            int result = 0;
            try
            {
                Init();
                result = conn.Insert(new Order { organizationID = org_id, sectorID = sector_id, receiverID = reciver_id, });
            } catch (Exception e) {
                Console.WriteLine("faild to add new order {0}", e.Message);
            }
            return result;
        }
        public List<Product> GetAllProduct()
        {
            try
            {
                Init();
                return this.conn.Table<Product>().ToList();
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Failed to retrieve data. {0}", ex.Message));
            }

            return new List<Product>();
        }
        public List<Organization> GetAllOrganization()
        {
            try
            {
                Init();
                return this.conn.Table<Organization>().ToList();
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Failed to retrieve data. {0}", ex.Message));
            }

            return new List<Organization>();
        }

        public List<Saller> GetAllSallers()
        {
            try
            {
                Init();
                return conn.Table<Saller>().ToList();
            }catch(Exception ex)
            {
                Console.WriteLine("faild to retrave sallers list {0}", ex.Message);
            }
            return new List<Saller>();

        }

        public List<string> GetOrganizationName()
        {

            try
            {
                Init();
                return this.conn.Table<Organization>().Select(o => o.OrganizationName).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Failed to retrieve data. {0}", ex.Message));
            }

            return new List<string>();
        }

        public List<string> GetAllSectorsName()
        {
            try
            {
                Init();
                return this.conn.Table<Sector>().Select(o => o.SectorName).ToList();
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Failed to retrieve Sector. {0}", ex.Message));
            }

            return new List<string>();
        }
        public List<Sector> GetAllSectors()
        {
            try
            {
                Init();
                return this.conn.Table<Sector>().ToList();
            }
            catch (Exception ex)
            {
                Console.Write(string.Format("Failed to retrieve Sector. {0}", ex.Message));
            }

            return new List<Sector>();
        }
        public List<Order> GetAllOrders()
        {
            try
            {
                Init();
                return this.conn.Table<Order>().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("faild to get all orders {0}", ex.Message);
            }

            return new List<Order>();
        }

        public int GetNumberOfOrders(int year)
        {
            int orders = -1;
            try
            {
                Init();
                orders =  this.conn.Table<Order>().Where(s=>s.year== year).ToList().Count;
                return orders;
            }
            catch(Exception ex)
            {
                Console.WriteLine("fiald to retraive orders {0}", ex.Message);
            }
            return orders;

        }

        public Delevary_product GetTheLastDelivaryProductByOrganization(int organizationID,int product_id)
        {
            try
            {
                Init();
                return conn.Table<Delevary_product>().Where(s => s.OrganizationId == organizationID && s.Product_id==product_id)
                    .OrderByDescending(item => item.Id)
                    .FirstOrDefault();
            }catch
            {
                return null;
            }
        }

        public Product GetProductByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return null;
                Init();
                name = name.ToLower();
                Product result = conn.Table<Product>().Where(item => item.Name == name).FirstOrDefault();
                if (result != null) return result;


            }
            catch (Exception ex)
            {
                Console.WriteLine("faild to retraive id product {0}", ex.Message);
            }

            return null;
        }
        public int GetIdSallerByName(string name)
        {
            try
            {
                if(string.IsNullOrEmpty(name)) return -1;

                Init();
                name = name.ToLower();
                Saller result = conn.Table<Saller>().Where(item => item.sallerName == name).FirstOrDefault();
                if (result != null) return result.sallerId;


            }
            catch (Exception ex)
            {
                Console.WriteLine("faild to retraive id saller {0}", ex.Message);
            }

            return -1;
        }
        public int GetIdSectorByName(string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name)) return -1;

                Init();
                name = name.ToLower();
                Sector result = conn.Table<Sector>().Where(item => item.SectorName == name).FirstOrDefault();
                if (result != null) return result.SectorId;


            }
            catch (Exception ex)
            {
                Console.WriteLine("faild to retraive id sector {0}", ex.Message);
            }

            return -1;
        }

    }
}

