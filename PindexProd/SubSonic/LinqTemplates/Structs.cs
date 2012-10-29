


using System;
using SubSonic.Schema;
using System.Collections.Generic;
using SubSonic.DataProviders;
using System.Data;

namespace SubSonic.POCOS1 {
	
        /// <summary>
        /// Table: CategoryImagesMapping
        /// Primary Key: ID
        /// </summary>

        public class CategoryImagesMappingTable: DatabaseTable {
            
            public CategoryImagesMappingTable(IDataProvider provider):base("CategoryImagesMapping",provider){
                ClassName = "CategoryImagesMapping";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("CategoryID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ImageID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn CategoryID{
                get{
                    return this.GetColumn("CategoryID");
                }
            }
            				
   			public static string CategoryIDColumn{
			      get{
        			return "CategoryID";
      			}
		    }
           
            public IColumn ImageID{
                get{
                    return this.GetColumn("ImageID");
                }
            }
            				
   			public static string ImageIDColumn{
			      get{
        			return "ImageID";
      			}
		    }
           
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: AppUsers
        /// Primary Key: ID
        /// </summary>

        public class AppUsersTable: DatabaseTable {
            
            public AppUsersTable(IDataProvider provider):base("AppUsers",provider){
                ClassName = "AppUser";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 64
                });

                Columns.Add(new DatabaseColumn("Password", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1024
                });

                Columns.Add(new DatabaseColumn("Email", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 128
                });

                Columns.Add(new DatabaseColumn("Avatar", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 128
                });

                Columns.Add(new DatabaseColumn("FirstName", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 64
                });

                Columns.Add(new DatabaseColumn("About", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 512
                });

                Columns.Add(new DatabaseColumn("Location", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 128
                });

                Columns.Add(new DatabaseColumn("Website", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("Invite", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("facebookid", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Points", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn Password{
                get{
                    return this.GetColumn("Password");
                }
            }
            				
   			public static string PasswordColumn{
			      get{
        			return "Password";
      			}
		    }
           
            public IColumn Email{
                get{
                    return this.GetColumn("Email");
                }
            }
            				
   			public static string EmailColumn{
			      get{
        			return "Email";
      			}
		    }
           
            public IColumn Avatar{
                get{
                    return this.GetColumn("Avatar");
                }
            }
            				
   			public static string AvatarColumn{
			      get{
        			return "Avatar";
      			}
		    }
           
            public IColumn FirstName{
                get{
                    return this.GetColumn("FirstName");
                }
            }
            				
   			public static string FirstNameColumn{
			      get{
        			return "FirstName";
      			}
		    }
           
            public IColumn About{
                get{
                    return this.GetColumn("About");
                }
            }
            				
   			public static string AboutColumn{
			      get{
        			return "About";
      			}
		    }
           
            public IColumn Location{
                get{
                    return this.GetColumn("Location");
                }
            }
            				
   			public static string LocationColumn{
			      get{
        			return "Location";
      			}
		    }
           
            public IColumn Website{
                get{
                    return this.GetColumn("Website");
                }
            }
            				
   			public static string WebsiteColumn{
			      get{
        			return "Website";
      			}
		    }
           
            public IColumn Invite{
                get{
                    return this.GetColumn("Invite");
                }
            }
            				
   			public static string InviteColumn{
			      get{
        			return "Invite";
      			}
		    }
           
            public IColumn facebookid{
                get{
                    return this.GetColumn("facebookid");
                }
            }
            				
   			public static string facebookidColumn{
			      get{
        			return "facebookid";
      			}
		    }
           
            public IColumn Points{
                get{
                    return this.GetColumn("Points");
                }
            }
            				
   			public static string PointsColumn{
			      get{
        			return "Points";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: User
        /// Primary Key: ID
        /// </summary>

        public class UserTable: DatabaseTable {
            
            public UserTable(IDataProvider provider):base("User",provider){
                ClassName = "User";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 64
                });

                Columns.Add(new DatabaseColumn("Password", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 128
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn Password{
                get{
                    return this.GetColumn("Password");
                }
            }
            				
   			public static string PasswordColumn{
			      get{
        			return "Password";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: UserBatchAssigned
        /// Primary Key: ID
        /// </summary>

        public class UserBatchAssignedTable: DatabaseTable {
            
            public UserBatchAssignedTable(IDataProvider provider):base("UserBatchAssigned",provider){
                ClassName = "UserBatchAssigned";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BatchStart", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BatchEnd", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn BatchStart{
                get{
                    return this.GetColumn("BatchStart");
                }
            }
            				
   			public static string BatchStartColumn{
			      get{
        			return "BatchStart";
      			}
		    }
           
            public IColumn BatchEnd{
                get{
                    return this.GetColumn("BatchEnd");
                }
            }
            				
   			public static string BatchEndColumn{
			      get{
        			return "BatchEnd";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Likes
        /// Primary Key: ID
        /// </summary>

        public class LikesTable: DatabaseTable {
            
            public LikesTable(IDataProvider provider):base("Likes",provider){
                ClassName = "Like";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BoardsImagesMappingID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn BoardsImagesMappingID{
                get{
                    return this.GetColumn("BoardsImagesMappingID");
                }
            }
            				
   			public static string BoardsImagesMappingIDColumn{
			      get{
        			return "BoardsImagesMappingID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Articles
        /// Primary Key: ID
        /// </summary>

        public class ArticlesTable: DatabaseTable {
            
            public ArticlesTable(IDataProvider provider):base("Articles",provider){
                ClassName = "Article";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("RelImagePath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1024
                });

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("Image_Height", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Image_Width", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FNV1a", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MURMUR2", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CRC64", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ArticleContent", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });

                Columns.Add(new DatabaseColumn("Description", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = -1
                });
                    
                
                
            }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
            				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
           
            public IColumn RelImagePath{
                get{
                    return this.GetColumn("RelImagePath");
                }
            }
            				
   			public static string RelImagePathColumn{
			      get{
        			return "RelImagePath";
      			}
		    }
           
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
            				
   			public static string UrlColumn{
			      get{
        			return "Url";
      			}
		    }
           
            public IColumn Image_Height{
                get{
                    return this.GetColumn("Image_Height");
                }
            }
            				
   			public static string Image_HeightColumn{
			      get{
        			return "Image_Height";
      			}
		    }
           
            public IColumn Image_Width{
                get{
                    return this.GetColumn("Image_Width");
                }
            }
            				
   			public static string Image_WidthColumn{
			      get{
        			return "Image_Width";
      			}
		    }
           
            public IColumn FNV1a{
                get{
                    return this.GetColumn("FNV1a");
                }
            }
            				
   			public static string FNV1aColumn{
			      get{
        			return "FNV1a";
      			}
		    }
           
            public IColumn MURMUR2{
                get{
                    return this.GetColumn("MURMUR2");
                }
            }
            				
   			public static string MURMUR2Column{
			      get{
        			return "MURMUR2";
      			}
		    }
           
            public IColumn CRC64{
                get{
                    return this.GetColumn("CRC64");
                }
            }
            				
   			public static string CRC64Column{
			      get{
        			return "CRC64";
      			}
		    }
           
            public IColumn ArticleContent{
                get{
                    return this.GetColumn("ArticleContent");
                }
            }
            				
   			public static string ArticleContentColumn{
			      get{
        			return "ArticleContent";
      			}
		    }
           
            public IColumn Description{
                get{
                    return this.GetColumn("Description");
                }
            }
            				
   			public static string DescriptionColumn{
			      get{
        			return "Description";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Rating
        /// Primary Key: ImageID
        /// </summary>

        public class RatingTable: DatabaseTable {
            
            public RatingTable(IDataProvider provider):base("Rating",provider){
                ClassName = "Rating";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ImageID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RePins", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Likes", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Rating", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ImageID{
                get{
                    return this.GetColumn("ImageID");
                }
            }
            				
   			public static string ImageIDColumn{
			      get{
        			return "ImageID";
      			}
		    }
           
            public IColumn RePins{
                get{
                    return this.GetColumn("RePins");
                }
            }
            				
   			public static string RePinsColumn{
			      get{
        			return "RePins";
      			}
		    }
           
            public IColumn Likes{
                get{
                    return this.GetColumn("Likes");
                }
            }
            				
   			public static string LikesColumn{
			      get{
        			return "Likes";
      			}
		    }
           
            public IColumn Rating{
                get{
                    return this.GetColumn("Rating");
                }
            }
            				
   			public static string RatingColumn{
			      get{
        			return "Rating";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Review
        /// Primary Key: ID
        /// </summary>

        public class ReviewTable: DatabaseTable {
            
            public ReviewTable(IDataProvider provider):base("Review",provider){
                ClassName = "Review";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("BIMID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Question", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1024
                });

                Columns.Add(new DatabaseColumn("Answer", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1024
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("SessionID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1024
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn BIMID{
                get{
                    return this.GetColumn("BIMID");
                }
            }
            				
   			public static string BIMIDColumn{
			      get{
        			return "BIMID";
      			}
		    }
           
            public IColumn Question{
                get{
                    return this.GetColumn("Question");
                }
            }
            				
   			public static string QuestionColumn{
			      get{
        			return "Question";
      			}
		    }
           
            public IColumn Answer{
                get{
                    return this.GetColumn("Answer");
                }
            }
            				
   			public static string AnswerColumn{
			      get{
        			return "Answer";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn SessionID{
                get{
                    return this.GetColumn("SessionID");
                }
            }
            				
   			public static string SessionIDColumn{
			      get{
        			return "SessionID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Stores
        /// Primary Key: ID
        /// </summary>

        public class StoresTable: DatabaseTable {
            
            public StoresTable(IDataProvider provider):base("Stores",provider){
                ClassName = "Store";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 255
                });

                Columns.Add(new DatabaseColumn("RelImagePath", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 1024
                });

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Url", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("Image_Height", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Image_Width", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FNV1a", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MURMUR2", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CRC64", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn Title{
                get{
                    return this.GetColumn("Title");
                }
            }
            				
   			public static string TitleColumn{
			      get{
        			return "Title";
      			}
		    }
           
            public IColumn RelImagePath{
                get{
                    return this.GetColumn("RelImagePath");
                }
            }
            				
   			public static string RelImagePathColumn{
			      get{
        			return "RelImagePath";
      			}
		    }
           
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Url{
                get{
                    return this.GetColumn("Url");
                }
            }
            				
   			public static string UrlColumn{
			      get{
        			return "Url";
      			}
		    }
           
            public IColumn Image_Height{
                get{
                    return this.GetColumn("Image_Height");
                }
            }
            				
   			public static string Image_HeightColumn{
			      get{
        			return "Image_Height";
      			}
		    }
           
            public IColumn Image_Width{
                get{
                    return this.GetColumn("Image_Width");
                }
            }
            				
   			public static string Image_WidthColumn{
			      get{
        			return "Image_Width";
      			}
		    }
           
            public IColumn FNV1a{
                get{
                    return this.GetColumn("FNV1a");
                }
            }
            				
   			public static string FNV1aColumn{
			      get{
        			return "FNV1a";
      			}
		    }
           
            public IColumn MURMUR2{
                get{
                    return this.GetColumn("MURMUR2");
                }
            }
            				
   			public static string MURMUR2Column{
			      get{
        			return "MURMUR2";
      			}
		    }
           
            public IColumn CRC64{
                get{
                    return this.GetColumn("CRC64");
                }
            }
            				
   			public static string CRC64Column{
			      get{
        			return "CRC64";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: BoardContributor
        /// Primary Key: ID
        /// </summary>

        public class BoardContributorTable: DatabaseTable {
            
            public BoardContributorTable(IDataProvider provider):base("BoardContributor",provider){
                ClassName = "BoardContributor";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("BoardID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ContributorID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn BoardID{
                get{
                    return this.GetColumn("BoardID");
                }
            }
            				
   			public static string BoardIDColumn{
			      get{
        			return "BoardID";
      			}
		    }
           
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn ContributorID{
                get{
                    return this.GetColumn("ContributorID");
                }
            }
            				
   			public static string ContributorIDColumn{
			      get{
        			return "ContributorID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Images
        /// Primary Key: ID
        /// </summary>

        public class ImagesTable: DatabaseTable {
            
            public ImagesTable(IDataProvider provider):base("Images",provider){
                ClassName = "Image";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Image_Height", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Image_Width", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int16,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("RelativeImage_Path", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("Date", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Tagged", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("CRC64", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("FNV1a", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("MURMUR2", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Decimal,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Uploaded", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Boolean,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Image_Height{
                get{
                    return this.GetColumn("Image_Height");
                }
            }
            				
   			public static string Image_HeightColumn{
			      get{
        			return "Image_Height";
      			}
		    }
           
            public IColumn Image_Width{
                get{
                    return this.GetColumn("Image_Width");
                }
            }
            				
   			public static string Image_WidthColumn{
			      get{
        			return "Image_Width";
      			}
		    }
           
            public IColumn RelativeImage_Path{
                get{
                    return this.GetColumn("RelativeImage_Path");
                }
            }
            				
   			public static string RelativeImage_PathColumn{
			      get{
        			return "RelativeImage_Path";
      			}
		    }
           
            public IColumn Date{
                get{
                    return this.GetColumn("Date");
                }
            }
            				
   			public static string DateColumn{
			      get{
        			return "Date";
      			}
		    }
           
            public IColumn Tagged{
                get{
                    return this.GetColumn("Tagged");
                }
            }
            				
   			public static string TaggedColumn{
			      get{
        			return "Tagged";
      			}
		    }
           
            public IColumn CRC64{
                get{
                    return this.GetColumn("CRC64");
                }
            }
            				
   			public static string CRC64Column{
			      get{
        			return "CRC64";
      			}
		    }
           
            public IColumn FNV1a{
                get{
                    return this.GetColumn("FNV1a");
                }
            }
            				
   			public static string FNV1aColumn{
			      get{
        			return "FNV1a";
      			}
		    }
           
            public IColumn MURMUR2{
                get{
                    return this.GetColumn("MURMUR2");
                }
            }
            				
   			public static string MURMUR2Column{
			      get{
        			return "MURMUR2";
      			}
		    }
           
            public IColumn Uploaded{
                get{
                    return this.GetColumn("Uploaded");
                }
            }
            				
   			public static string UploadedColumn{
			      get{
        			return "Uploaded";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Boards
        /// Primary Key: ID
        /// </summary>

        public class BoardsTable: DatabaseTable {
            
            public BoardsTable(IDataProvider provider):base("Boards",provider){
                ClassName = "Board";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 64
                });

                Columns.Add(new DatabaseColumn("CatID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn CatID{
                get{
                    return this.GetColumn("CatID");
                }
            }
            				
   			public static string CatIDColumn{
			      get{
        			return "CatID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: BoardsImagesMapping
        /// Primary Key: ID
        /// </summary>

        public class BoardsImagesMappingTable: DatabaseTable {
            
            public BoardsImagesMappingTable(IDataProvider provider):base("BoardsImagesMapping",provider){
                ClassName = "BoardsImagesMapping";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("BoardID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ImageID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Image_Title", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Source", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("Rating", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn BoardID{
                get{
                    return this.GetColumn("BoardID");
                }
            }
            				
   			public static string BoardIDColumn{
			      get{
        			return "BoardID";
      			}
		    }
           
            public IColumn ImageID{
                get{
                    return this.GetColumn("ImageID");
                }
            }
            				
   			public static string ImageIDColumn{
			      get{
        			return "ImageID";
      			}
		    }
           
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Image_Title{
                get{
                    return this.GetColumn("Image_Title");
                }
            }
            				
   			public static string Image_TitleColumn{
			      get{
        			return "Image_Title";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
            public IColumn Source{
                get{
                    return this.GetColumn("Source");
                }
            }
            				
   			public static string SourceColumn{
			      get{
        			return "Source";
      			}
		    }
           
            public IColumn Rating{
                get{
                    return this.GetColumn("Rating");
                }
            }
            				
   			public static string RatingColumn{
			      get{
        			return "Rating";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Comments
        /// Primary Key: ID
        /// </summary>

        public class CommentsTable: DatabaseTable {
            
            public CommentsTable(IDataProvider provider):base("Comments",provider){
                ClassName = "Comment";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Comments", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 512
                });

                Columns.Add(new DatabaseColumn("BoardsImagesMappingID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("UserID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Comments{
                get{
                    return this.GetColumn("Comments");
                }
            }
            				
   			public static string CommentsColumn{
			      get{
        			return "Comments";
      			}
		    }
           
            public IColumn BoardsImagesMappingID{
                get{
                    return this.GetColumn("BoardsImagesMappingID");
                }
            }
            				
   			public static string BoardsImagesMappingIDColumn{
			      get{
        			return "BoardsImagesMappingID";
      			}
		    }
           
            public IColumn UserID{
                get{
                    return this.GetColumn("UserID");
                }
            }
            				
   			public static string UserIDColumn{
			      get{
        			return "UserID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Category
        /// Primary Key: ID
        /// </summary>

        public class CategoryTable: DatabaseTable {
            
            public CategoryTable(IDataProvider provider):base("Category",provider){
                ClassName = "Category";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("ID", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Int32,
	                IsNullable = false,
	                AutoIncrement = true,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("Name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 64
                });

                Columns.Add(new DatabaseColumn("ParentID", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Int32,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn ID{
                get{
                    return this.GetColumn("ID");
                }
            }
            				
   			public static string IDColumn{
			      get{
        			return "ID";
      			}
		    }
           
            public IColumn Name{
                get{
                    return this.GetColumn("Name");
                }
            }
            				
   			public static string NameColumn{
			      get{
        			return "Name";
      			}
		    }
           
            public IColumn ParentID{
                get{
                    return this.GetColumn("ParentID");
                }
            }
            				
   			public static string ParentIDColumn{
			      get{
        			return "ParentID";
      			}
		    }
           
                    
        }
        
        /// <summary>
        /// Table: Facebook
        /// Primary Key: id
        /// </summary>

        public class FacebookTable: DatabaseTable {
            
            public FacebookTable(IDataProvider provider):base("Facebook",provider){
                ClassName = "Facebook";
                SchemaName = "dbo";
                

                Columns.Add(new DatabaseColumn("first_name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 128
                });

                Columns.Add(new DatabaseColumn("gender", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 8
                });

                Columns.Add(new DatabaseColumn("id", this)
                {
	                IsPrimaryKey = true,
	                DataType = DbType.Decimal,
	                IsNullable = false,
	                AutoIncrement = false,
	                IsForeignKey = true,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("last_name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 128
                });

                Columns.Add(new DatabaseColumn("link", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 2048
                });

                Columns.Add(new DatabaseColumn("locale", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 8
                });

                Columns.Add(new DatabaseColumn("name", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.AnsiString,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 256
                });

                Columns.Add(new DatabaseColumn("timezone", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.Double,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });

                Columns.Add(new DatabaseColumn("updated_time", this)
                {
	                IsPrimaryKey = false,
	                DataType = DbType.DateTime,
	                IsNullable = true,
	                AutoIncrement = false,
	                IsForeignKey = false,
	                MaxLength = 0
                });
                    
                
                
            }
            
            public IColumn first_name{
                get{
                    return this.GetColumn("first_name");
                }
            }
            				
   			public static string first_nameColumn{
			      get{
        			return "first_name";
      			}
		    }
           
            public IColumn gender{
                get{
                    return this.GetColumn("gender");
                }
            }
            				
   			public static string genderColumn{
			      get{
        			return "gender";
      			}
		    }
           
            public IColumn id{
                get{
                    return this.GetColumn("id");
                }
            }
            				
   			public static string idColumn{
			      get{
        			return "id";
      			}
		    }
           
            public IColumn last_name{
                get{
                    return this.GetColumn("last_name");
                }
            }
            				
   			public static string last_nameColumn{
			      get{
        			return "last_name";
      			}
		    }
           
            public IColumn link{
                get{
                    return this.GetColumn("link");
                }
            }
            				
   			public static string linkColumn{
			      get{
        			return "link";
      			}
		    }
           
            public IColumn locale{
                get{
                    return this.GetColumn("locale");
                }
            }
            				
   			public static string localeColumn{
			      get{
        			return "locale";
      			}
		    }
           
            public IColumn name{
                get{
                    return this.GetColumn("name");
                }
            }
            				
   			public static string nameColumn{
			      get{
        			return "name";
      			}
		    }
           
            public IColumn timezone{
                get{
                    return this.GetColumn("timezone");
                }
            }
            				
   			public static string timezoneColumn{
			      get{
        			return "timezone";
      			}
		    }
           
            public IColumn updated_time{
                get{
                    return this.GetColumn("updated_time");
                }
            }
            				
   			public static string updated_timeColumn{
			      get{
        			return "updated_time";
      			}
		    }
           
                    
        }
        
}