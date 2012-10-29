


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace SubSonic.POCOS1{
	public partial class PinjimuDB{

        public StoredProcedure AddBoardContributor(string Email,int BoardID){
            StoredProcedure sp=new StoredProcedure("AddBoardContributor",this.Provider);
            sp.Command.AddParameter("Email",Email,DbType.AnsiString);
            sp.Command.AddParameter("BoardID",BoardID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteBoard(int boardid){
            StoredProcedure sp=new StoredProcedure("DeleteBoard",this.Provider);
            sp.Command.AddParameter("boardid",boardid,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteBoardContributor(string Email,int BoardID){
            StoredProcedure sp=new StoredProcedure("DeleteBoardContributor",this.Provider);
            sp.Command.AddParameter("Email",Email,DbType.AnsiString);
            sp.Command.AddParameter("BoardID",BoardID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeletePin(int BIMID,int UserID){
            StoredProcedure sp=new StoredProcedure("DeletePin",this.Provider);
            sp.Command.AddParameter("BIMID",BIMID,DbType.Int32);
            sp.Command.AddParameter("UserID",UserID,DbType.Int32);
            return sp;
        }
        public StoredProcedure SetTaggedFlag(int ID){
            StoredProcedure sp=new StoredProcedure("SetTaggedFlag",this.Provider);
            sp.Command.AddParameter("ID",ID,DbType.Int32);
            return sp;
        }
        public StoredProcedure UpdatePoints(int UserID,string PointsName){
            StoredProcedure sp=new StoredProcedure("UpdatePoints",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.Int32);
            sp.Command.AddParameter("PointsName",PointsName,DbType.AnsiString);
            return sp;
        }
        public StoredProcedure UpdatePrize(int UserID,int RWPointDed){
            StoredProcedure sp=new StoredProcedure("UpdatePrize",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.Int32);
            sp.Command.AddParameter("RWPointDed",RWPointDed,DbType.Int32);
            return sp;
        }
	
	}
	
}
 