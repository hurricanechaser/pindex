


  
using System;
using SubSonic;
using SubSonic.Schema;
using SubSonic.DataProviders;
using System.Data;

namespace SubSonic.POCOS{
	public partial class PindexProdDB{

        public StoredProcedure AddBoardContributor(string User,int BoardID){
            StoredProcedure sp=new StoredProcedure("AddBoardContributor",this.Provider);
            sp.Command.AddParameter("User",User,DbType.AnsiString);
            sp.Command.AddParameter("BoardID",BoardID,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteBoard(int boardid){
            StoredProcedure sp=new StoredProcedure("DeleteBoard",this.Provider);
            sp.Command.AddParameter("boardid",boardid,DbType.Int32);
            return sp;
        }
        public StoredProcedure DeleteBoardContributor(string User,int BoardID){
            StoredProcedure sp=new StoredProcedure("DeleteBoardContributor",this.Provider);
            sp.Command.AddParameter("User",User,DbType.AnsiString);
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
        public StoredProcedure UpdatePoints(int UserID,string SessionID){
            StoredProcedure sp=new StoredProcedure("UpdatePoints",this.Provider);
            sp.Command.AddParameter("UserID",UserID,DbType.Int32);
            sp.Command.AddParameter("SessionID",SessionID,DbType.AnsiString);
            return sp;
        }
	
	}
	
}
 