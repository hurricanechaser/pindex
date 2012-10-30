


  
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
        public StoredProcedure Import(short Image_Height,short Image_Width,string RelativeImage_Path,decimal CRC64,decimal FNV1a,decimal MURMUR2,int Category,int Style,string Image_Title,string Source,int Rating){
            StoredProcedure sp=new StoredProcedure("Import",this.Provider);
            sp.Command.AddParameter("Image_Height",Image_Height,DbType.Int16);
            sp.Command.AddParameter("Image_Width",Image_Width,DbType.Int16);
            sp.Command.AddParameter("RelativeImage_Path",RelativeImage_Path,DbType.AnsiString);
            sp.Command.AddParameter("CRC64",CRC64,DbType.Decimal);
            sp.Command.AddParameter("FNV1a",FNV1a,DbType.Decimal);
            sp.Command.AddParameter("MURMUR2",MURMUR2,DbType.Decimal);
            sp.Command.AddParameter("Category",Category,DbType.Int32);
            sp.Command.AddParameter("Style",Style,DbType.Int32);
            sp.Command.AddParameter("Image_Title",Image_Title,DbType.String);
            sp.Command.AddParameter("Source",Source,DbType.AnsiString);
            sp.Command.AddParameter("Rating",Rating,DbType.Int32);
            return sp;
        }
        public StoredProcedure RethrowError(){
            StoredProcedure sp=new StoredProcedure("RethrowError",this.Provider);
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
 