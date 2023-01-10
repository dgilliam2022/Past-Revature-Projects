using Models;
using System.Data.SqlClient;
using DataAccess.Entities;
using CustomExceptions;

namespace DataAccess;

public class ResourceRepo : IResourceGen
{
    private readonly wearelosingsteamContext _context;

    public ResourceRepo (wearelosingsteamContext context)
    {
        _context = context;
    }
    /*/// <summary>
    /// Add a certain amount of gold to a user
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>
    /// <exception cref="ResourceNotFound">Occurs if no user exist matching the given User object</exception>*/
    public bool AddGold(User User, int Amount)
    {
        User User2Add2 = _context.Users.Find(User.UserId); //let us hope this works
        if(User2Add2 == null) //such user does not exist
        {
            throw new ResourceNotFound();
        }
        
        User2Add2.GoldCount = User2Add2.GoldCount + Amount; //make the change
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
    /*/// <summary>
    /// Add a certain amount of Egg to an user
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>
    /// <exception cref="ResourceNotFound">Occurs if no user exist matching the given User object</exception>*/
    public bool AddEgg(User User, int Amount)
    {   
        User User2Add2 = _context.Users.Find(User.UserId); //let us hope this works
        if(User2Add2 == null) //such user does not exist
        {
            throw new ResourceNotFound();
        }

        User2Add2.EggCount = User2Add2.EggCount + Amount; //make the change
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }
    /*/// <summary>
    /// Attach a sudo-random amount of one random food type to a certain user's inventory
    /// </summary>
    /// <param name="User"></param>
    /// <param name="Amount"></param>
    /// <param name="Food2Add"></param>
    /// <returns>1 if operation is successful, it will never return false at the moment</returns>*/
    public bool AddFood(User User, int Weight) //remember to check for input validation in the services layer
    {
        Random randomStat = new Random(); //I didn't copy homework from Lor's methods I swear
        int Amount = 1; //default to 1 for now
        int seed = randomStat.Next(1, Weight); //this weights the amounts of food anyone can get, should spice things up

        FoodStat Food2Add = _context.FoodStats.Find(randomStat.Next(1, 9)); //Our foodstat table has non consecutive IDs, so this will work weird
        while(Food2Add == null) //Hopefully this fixes above issue
        {
            Food2Add = _context.FoodStats.Find(randomStat.Next(1, 9));
        }
        if(Food2Add == null)
        {
            return false; //if somehow the while loop failed, return exit failure
        }

        if(seed <= 10) //May the RNGesus bless you
        {
            Amount = 0;
        }
        else if(seed<= 30) //this is also exponential scaling, STFU
        {
            Amount = 1;
        }
        else if(seed<= 90)
        {
            Amount = 2;
        }
        else if(seed<= 270)
        {
            Amount = 3;
        }
        else
        {
            Amount = 0;
        }

        FoodInventory Inventory2Add2 = 
        (from IV in _context.FoodInventories
        where (IV.UserIdFk == User.UserId) && (IV.FoodStatsIdFk == Food2Add.FoodStatsId)
        select IV).FirstOrDefault(); //this whole thing returns either a foodInvertory or null

        if(Inventory2Add2 == null) //if user does not have this kind of food yet
        {
            Inventory2Add2 = new FoodInventory
            {
                UserIdFk = (int) User.UserId,
                FoodStatsIdFk = Food2Add.FoodStatsId,
                FoodCount = Amount
            };
            _context.Add(Inventory2Add2); //Add a new item onto the table
            _context.SaveChanges(); //persist the change
            _context.ChangeTracker.Clear(); //clear the tracker for the next person
            return true;
        }

        Inventory2Add2.FoodCount = Inventory2Add2.FoodCount + Amount; //if the user already own this kind of food
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next person
        return true;
    }

    /*/// <summary>
    /// Remove a certain amount of food from the food inventory attached to the user
    /// </summary>
    /// <param name="UserID"></param>
    /// <param name="FoodID"></param>
    /// <returns></returns>*/
    public bool RemoveFood(int userId, int foodId)
    {
        User userInfo = _context.Users.Find(userId);
        if (userInfo == null) //such user does not exist
        {
            throw new UserNotFound();
        }
        FoodInventory foodInfo = _context.FoodInventories.Find(userInfo.UserId, foodId); //the primary key for this table is composite key
        if (foodInfo == null || foodInfo.FoodCount == 0) //that user does not own that food at all or has none of that food left in their inventory
        {
            throw new FoodNotFound();
        }
        foodInfo.FoodCount = foodInfo.FoodCount - 1; 
        _context.SaveChanges(); //persist the change
        _context.ChangeTracker.Clear(); //clear the tracker for the next food transaction
        return true;
    }

    public List<FoodInventory> GetFoodInventoryByUserId(int userId)
    {
        return _context.FoodInventories.Where(food => food.UserIdFk == userId).ToList();
    }

    public List<FoodInventory> Purchase(int userId, int[] foodQtyArr, int eggQty) //foodIdArr = foodTypes(got rid of this), foodQtyArr = amounts, eggQty = egg amount
    {
        User userToBuy = _context.Users.Find(userId);                             //define a user who will purchase food.
        if (userToBuy == null) //such user does not exist
        {
            throw new UserNotFound();
        }

        int totalFoodCost = ((foodQtyArr.Sum()) * 10);                            //each food costs 10 gold.

        Console.WriteLine(totalFoodCost);                                         //for testing

        int eggCost = (eggQty * 100);                                                     //eggs each cost 100 gold.

        int totalCost = ((totalFoodCost + eggCost) * -1);                                  //Stores total gold cost to remove from user.     

        if((userToBuy.GoldCount + totalCost) < 0)                                       //checking if user has the right amount of gold
        {
            throw new InsufficientFunds();                                        //throws exception if not
        }
        else
        {
            userToBuy.GoldCount =  userToBuy.GoldCount + totalCost;               //subtracts total from user gold if applicable
        }        

        AddEgg(userToBuy, eggQty);                                          //adds eggs to user's inventory if they have enough gold.
                                                  
        int k = 0;

        for(int i = 1; i <= 6; i++)                                                //Total food purchased will be added to this at the end.
        { 
            FoodInventory userFoodInstance = _context.FoodInventories.Find(userToBuy.UserId, i);//i also serving as food element as it iterates
            Console.WriteLine(userFoodInstance);
            if (userFoodInstance == null)                                                               //For each element of the List, 0 if currently null.
            {
                FoodInventory newFood = new FoodInventory()
                {
                    UserIdFk = (int)userToBuy.UserId,
                    FoodStatsIdFk = i,
                    FoodCount = 0
                };
                _context.FoodInventories.Add(newFood);
                userFoodInstance = newFood;
            }

            userFoodInstance.FoodCount = userFoodInstance.FoodCount + foodQtyArr[k];                //adds qty of food to foodInstance in foodList if applicable
            Console.WriteLine(k);

            k++;
        }                               

        _context.SaveChanges();                              

        _context.ChangeTracker.Clear();

        return GetFoodInventoryByUserId(userId);  
        //current full food inventory           -done
        //take types of foods to be purchased   -done
        //take qtys of foods to be purchased    -done
        //take qty of eggs to be purchased      -EZ
        //calculate gold cost                   -done
        //calculate egg cost                    -done
        //check to see if user has totalCost    -done
        //throw exception if not                -done
        //remove gold from user                 -done
        //add items to user inventory           -
        //add eggs to user inventory            -done
        //save changes                          -done
    }
}