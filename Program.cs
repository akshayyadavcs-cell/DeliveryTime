using System;
using System.Collections.Generic;
using System.Linq;

namespace DeliveryCost
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Creating Input Object");

            Shipment oShipment = new Shipment();
            oShipment.baseDeliveryCost = 100;
            oShipment.numberOfPackages = 3;

            Package oPackage1 = new Package();
            oPackage1.packageID = "PKG1";
            oPackage1.packageWeight = 5;
            oPackage1.packageDistance = 5;
            oPackage1.offerID = "OFR001";

            Package oPackage2 = new Package();
            oPackage2.packageID = "PKG2";
            oPackage2.packageWeight = 15;
            oPackage2.packageDistance = 5;
            oPackage2.offerID = "OFR002";

            Package oPackage3 = new Package();
            oPackage3.packageID = "PKG3";
            oPackage3.packageWeight = 10;
            oPackage3.packageDistance = 100;
            oPackage3.offerID = "OFR003";

            DeliveryMethods deliveryMethods = new DeliveryMethods();




            //Output Package Cost is here 

            PackageCost packageCost1 = deliveryMethods.CalculateCost(oShipment.baseDeliveryCost, oPackage1);
            PackageCost packageCost2 = deliveryMethods.CalculateCost(oShipment.baseDeliveryCost, oPackage2);
            PackageCost packageCost3 = deliveryMethods.CalculateCost(oShipment.baseDeliveryCost, oPackage3);

            Console.WriteLine(packageCost1);
            Console.ReadLine();

        }
    }
    public class Offers
    {
        public string offerID = string.Empty;
        public double offerDiscount = 0;
        public int minDistance = 0;
        public int maxDistance = 0;
        public int minWeight = 0;
        public int maxWeight = 0;

    }
    public class Package
    {
        public string packageID = string.Empty;
        public int packageWeight = 0;
        public int packageDistance = 0;
        public string offerID = string.Empty;

    }
    public class PackageCost
    {
        public string packageID = string.Empty;
        public double packageDiscount = 0;
        public int totalCost = 0;

    }

    public class Shipment
    {
        public int baseDeliveryCost = 0;
        public int numberOfPackages = 0;
        public List<Package> lstPackages = new List<Package>();
    }

    public class DeliveryMethods
    {
        public PackageCost CalculateCost(int iBaseDeliveryCost,Package oPackage)
        {
            int iCost = iBaseDeliveryCost + (oPackage.packageWeight * 10) + (oPackage.packageDistance*5);
            int iDiscount = 0;
            if (GetDiscout(oPackage) >0)
            {
                iDiscount = Convert.ToInt32 ( (GetDiscout(oPackage) * iCost) / 100);
                iCost = iCost - iDiscount;
            }
            
            PackageCost packageCost = new PackageCost();
            packageCost.packageID = oPackage.packageID;
            packageCost.totalCost = iCost;
            packageCost.packageDiscount = iDiscount;


            return packageCost;

        }
        public Double GetDiscout(Package oPackage)
        {
            List<Offers> lstOffers = GetOfferList();

            Offers _offer = lstOffers.Single(o => o.offerID == oPackage.offerID);
            if (oPackage.packageDistance>= _offer.minDistance && oPackage.packageDistance<= _offer.maxDistance && oPackage.packageWeight>= _offer.minWeight && oPackage.packageWeight <= _offer.maxWeight)
            {
                return _offer.offerDiscount;
            }
            else
            {
                return 0;

            }


            
        }

        public List<Offers> GetOfferList()
        {
            List<Offers> lstOffers = new List<Offers>();

            Offers _offer1 = new Offers();

            _offer1.offerID = "OFR001";
            _offer1.offerDiscount = 10;
            _offer1.minDistance = 0;
            _offer1.maxDistance = 199;
            _offer1.minWeight = 70;
            _offer1.maxWeight = 200;

            Offers _offer2 = new Offers();

            _offer2.offerID = "OFR002";
            _offer2.offerDiscount = 7;
            _offer2.minDistance = 50;
            _offer2.maxDistance = 150;
            _offer2.minWeight = 100;
            _offer2.maxWeight = 250;

            Offers _offer3 = new Offers();

            _offer3.offerID = "OFR003";
            _offer3.offerDiscount = 5;
            _offer3.minDistance = 50;
            _offer3.maxDistance = 250;
            _offer3.minWeight = 10;
            _offer3.maxWeight = 150;


            lstOffers.Add(_offer1);
            lstOffers.Add(_offer2);
            lstOffers.Add(_offer3);

            return lstOffers;
        }
    }
}
    
   


