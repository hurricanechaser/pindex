


using System;
using System.ComponentModel;
using System.Linq;

namespace SubSonic.POCOS1
{
    
    
    
    
    /// <summary>
    /// A class which represents the CategoryImagesMapping table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.CategoryImagesMapping 
    /// </summary>

	public partial class CategoryImagesMapping: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public CategoryImagesMapping(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnCategoryIDChanging(int value);
        partial void OnCategoryIDChanged();
		
		private int _CategoryID;
		public int CategoryID { 
		    get{
		        return _CategoryID;
		    } 
		    set{
		        this.OnCategoryIDChanging(value);
                this.SendPropertyChanging();
                this._CategoryID = value;
                this.SendPropertyChanged("CategoryID");
                this.OnCategoryIDChanged();
		    }
		}
		
        partial void OnImageIDChanging(int value);
        partial void OnImageIDChanged();
		
		private int _ImageID;
		public int ImageID { 
		    get{
		        return _ImageID;
		    } 
		    set{
		        this.OnImageIDChanging(value);
                this.SendPropertyChanging();
                this._ImageID = value;
                this.SendPropertyChanged("ImageID");
                this.OnImageIDChanged();
		    }
		}
		
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(int? value);
        partial void OnUserIDChanged();
		
		private int? _UserID;
		public int? UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Category> Categories
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Categories
                       where items.ID == _CategoryID
                       select items;
            }
        }

        public IQueryable<Image> Images
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Images
                       where items.ID == _ImageID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the AppUsers table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.AppUser 
    /// </summary>

	public partial class AppUser: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public AppUser(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
		
		private string _Password;
		public string Password { 
		    get{
		        return _Password;
		    } 
		    set{
		        this.OnPasswordChanging(value);
                this.SendPropertyChanging();
                this._Password = value;
                this.SendPropertyChanged("Password");
                this.OnPasswordChanged();
		    }
		}
		
        partial void OnEmailChanging(string value);
        partial void OnEmailChanged();
		
		private string _Email;
		public string Email { 
		    get{
		        return _Email;
		    } 
		    set{
		        this.OnEmailChanging(value);
                this.SendPropertyChanging();
                this._Email = value;
                this.SendPropertyChanged("Email");
                this.OnEmailChanged();
		    }
		}
		
        partial void OnAvatarChanging(string value);
        partial void OnAvatarChanged();
		
		private string _Avatar;
		public string Avatar { 
		    get{
		        return _Avatar;
		    } 
		    set{
		        this.OnAvatarChanging(value);
                this.SendPropertyChanging();
                this._Avatar = value;
                this.SendPropertyChanged("Avatar");
                this.OnAvatarChanged();
		    }
		}
		
        partial void OnFirstNameChanging(string value);
        partial void OnFirstNameChanged();
		
		private string _FirstName;
		public string FirstName { 
		    get{
		        return _FirstName;
		    } 
		    set{
		        this.OnFirstNameChanging(value);
                this.SendPropertyChanging();
                this._FirstName = value;
                this.SendPropertyChanged("FirstName");
                this.OnFirstNameChanged();
		    }
		}
		
        partial void OnAboutChanging(string value);
        partial void OnAboutChanged();
		
		private string _About;
		public string About { 
		    get{
		        return _About;
		    } 
		    set{
		        this.OnAboutChanging(value);
                this.SendPropertyChanging();
                this._About = value;
                this.SendPropertyChanged("About");
                this.OnAboutChanged();
		    }
		}
		
        partial void OnLocationChanging(string value);
        partial void OnLocationChanged();
		
		private string _Location;
		public string Location { 
		    get{
		        return _Location;
		    } 
		    set{
		        this.OnLocationChanging(value);
                this.SendPropertyChanging();
                this._Location = value;
                this.SendPropertyChanged("Location");
                this.OnLocationChanged();
		    }
		}
		
        partial void OnWebsiteChanging(string value);
        partial void OnWebsiteChanged();
		
		private string _Website;
		public string Website { 
		    get{
		        return _Website;
		    } 
		    set{
		        this.OnWebsiteChanging(value);
                this.SendPropertyChanging();
                this._Website = value;
                this.SendPropertyChanged("Website");
                this.OnWebsiteChanged();
		    }
		}
		
        partial void OnInviteChanging(string value);
        partial void OnInviteChanged();
		
		private string _Invite;
		public string Invite { 
		    get{
		        return _Invite;
		    } 
		    set{
		        this.OnInviteChanging(value);
                this.SendPropertyChanging();
                this._Invite = value;
                this.SendPropertyChanged("Invite");
                this.OnInviteChanged();
		    }
		}
		
        partial void OnfacebookidChanging(decimal? value);
        partial void OnfacebookidChanged();
		
		private decimal? _facebookid;
		public decimal? facebookid { 
		    get{
		        return _facebookid;
		    } 
		    set{
		        this.OnfacebookidChanging(value);
                this.SendPropertyChanging();
                this._facebookid = value;
                this.SendPropertyChanged("facebookid");
                this.OnfacebookidChanged();
		    }
		}
		
        partial void OnPointsChanging(int? value);
        partial void OnPointsChanged();
		
		private int? _Points;
		public int? Points { 
		    get{
		        return _Points;
		    } 
		    set{
		        this.OnPointsChanging(value);
                this.SendPropertyChanging();
                this._Points = value;
                this.SendPropertyChanged("Points");
                this.OnPointsChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<BoardContributor> BoardContributors
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardContributors
                       where items.ContributorID == _ID
                       select items;
            }
        }

        public IQueryable<Board> Boards
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Boards
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardsImagesMappings
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Comments
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Like> Likes
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Likes
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Review> Reviews
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Reviews
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Facebook> Facebooks
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Facebooks
                       where items.id == _facebookid
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the User table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.User 
    /// </summary>

	public partial class User: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public User(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnPasswordChanging(string value);
        partial void OnPasswordChanged();
		
		private string _Password;
		public string Password { 
		    get{
		        return _Password;
		    } 
		    set{
		        this.OnPasswordChanging(value);
                this.SendPropertyChanging();
                this._Password = value;
                this.SendPropertyChanged("Password");
                this.OnPasswordChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<UserBatchAssigned> UserBatchAssigneds
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.UserBatchAssigneds
                       where items.UserID == _ID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the UserBatchAssigned table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.UserBatchAssigned 
    /// </summary>

	public partial class UserBatchAssigned: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public UserBatchAssigned(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(int value);
        partial void OnUserIDChanged();
		
		private int _UserID;
		public int UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		
        partial void OnBatchStartChanging(int value);
        partial void OnBatchStartChanged();
		
		private int _BatchStart;
		public int BatchStart { 
		    get{
		        return _BatchStart;
		    } 
		    set{
		        this.OnBatchStartChanging(value);
                this.SendPropertyChanging();
                this._BatchStart = value;
                this.SendPropertyChanged("BatchStart");
                this.OnBatchStartChanged();
		    }
		}
		
        partial void OnBatchEndChanging(int value);
        partial void OnBatchEndChanged();
		
		private int _BatchEnd;
		public int BatchEnd { 
		    get{
		        return _BatchEnd;
		    } 
		    set{
		        this.OnBatchEndChanging(value);
                this.SendPropertyChanging();
                this._BatchEnd = value;
                this.SendPropertyChanged("BatchEnd");
                this.OnBatchEndChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<User> Users
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Users
                       where items.ID == _UserID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Likes table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Like 
    /// </summary>

	public partial class Like: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Like(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnBoardsImagesMappingIDChanging(int value);
        partial void OnBoardsImagesMappingIDChanged();
		
		private int _BoardsImagesMappingID;
		public int BoardsImagesMappingID { 
		    get{
		        return _BoardsImagesMappingID;
		    } 
		    set{
		        this.OnBoardsImagesMappingIDChanging(value);
                this.SendPropertyChanging();
                this._BoardsImagesMappingID = value;
                this.SendPropertyChanged("BoardsImagesMappingID");
                this.OnBoardsImagesMappingIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(int value);
        partial void OnUserIDChanged();
		
		private int _UserID;
		public int UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardsImagesMappings
                       where items.ID == _BoardsImagesMappingID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Articles table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Article 
    /// </summary>

	public partial class Article: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Article(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
		
		private string _Title;
		public string Title { 
		    get{
		        return _Title;
		    } 
		    set{
		        this.OnTitleChanging(value);
                this.SendPropertyChanging();
                this._Title = value;
                this.SendPropertyChanged("Title");
                this.OnTitleChanged();
		    }
		}
		
        partial void OnRelImagePathChanging(string value);
        partial void OnRelImagePathChanged();
		
		private string _RelImagePath;
		public string RelImagePath { 
		    get{
		        return _RelImagePath;
		    } 
		    set{
		        this.OnRelImagePathChanging(value);
                this.SendPropertyChanging();
                this._RelImagePath = value;
                this.SendPropertyChanged("RelImagePath");
                this.OnRelImagePathChanged();
		    }
		}
		
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnUrlChanging(string value);
        partial void OnUrlChanged();
		
		private string _Url;
		public string Url { 
		    get{
		        return _Url;
		    } 
		    set{
		        this.OnUrlChanging(value);
                this.SendPropertyChanging();
                this._Url = value;
                this.SendPropertyChanged("Url");
                this.OnUrlChanged();
		    }
		}
		
        partial void OnImage_HeightChanging(short? value);
        partial void OnImage_HeightChanged();
		
		private short? _Image_Height;
		public short? Image_Height { 
		    get{
		        return _Image_Height;
		    } 
		    set{
		        this.OnImage_HeightChanging(value);
                this.SendPropertyChanging();
                this._Image_Height = value;
                this.SendPropertyChanged("Image_Height");
                this.OnImage_HeightChanged();
		    }
		}
		
        partial void OnImage_WidthChanging(short? value);
        partial void OnImage_WidthChanged();
		
		private short? _Image_Width;
		public short? Image_Width { 
		    get{
		        return _Image_Width;
		    } 
		    set{
		        this.OnImage_WidthChanging(value);
                this.SendPropertyChanging();
                this._Image_Width = value;
                this.SendPropertyChanged("Image_Width");
                this.OnImage_WidthChanged();
		    }
		}
		
        partial void OnFNV1aChanging(decimal? value);
        partial void OnFNV1aChanged();
		
		private decimal? _FNV1a;
		public decimal? FNV1a { 
		    get{
		        return _FNV1a;
		    } 
		    set{
		        this.OnFNV1aChanging(value);
                this.SendPropertyChanging();
                this._FNV1a = value;
                this.SendPropertyChanged("FNV1a");
                this.OnFNV1aChanged();
		    }
		}
		
        partial void OnMURMUR2Changing(decimal? value);
        partial void OnMURMUR2Changed();
		
		private decimal? _MURMUR2;
		public decimal? MURMUR2 { 
		    get{
		        return _MURMUR2;
		    } 
		    set{
		        this.OnMURMUR2Changing(value);
                this.SendPropertyChanging();
                this._MURMUR2 = value;
                this.SendPropertyChanged("MURMUR2");
                this.OnMURMUR2Changed();
		    }
		}
		
        partial void OnCRC64Changing(decimal? value);
        partial void OnCRC64Changed();
		
		private decimal? _CRC64;
		public decimal? CRC64 { 
		    get{
		        return _CRC64;
		    } 
		    set{
		        this.OnCRC64Changing(value);
                this.SendPropertyChanging();
                this._CRC64 = value;
                this.SendPropertyChanged("CRC64");
                this.OnCRC64Changed();
		    }
		}
		
        partial void OnArticleContentChanging(string value);
        partial void OnArticleContentChanged();
		
		private string _ArticleContent;
		public string ArticleContent { 
		    get{
		        return _ArticleContent;
		    } 
		    set{
		        this.OnArticleContentChanging(value);
                this.SendPropertyChanging();
                this._ArticleContent = value;
                this.SendPropertyChanged("ArticleContent");
                this.OnArticleContentChanged();
		    }
		}
		
        partial void OnDescriptionChanging(string value);
        partial void OnDescriptionChanged();
		
		private string _Description;
		public string Description { 
		    get{
		        return _Description;
		    } 
		    set{
		        this.OnDescriptionChanging(value);
                this.SendPropertyChanging();
                this._Description = value;
                this.SendPropertyChanged("Description");
                this.OnDescriptionChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the sysdiagrams table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.sysdiagram 
    /// </summary>

	public partial class sysdiagram: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public sysdiagram(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnnameChanging(string value);
        partial void OnnameChanged();
		
		private string _name;
		public string name { 
		    get{
		        return _name;
		    } 
		    set{
		        this.OnnameChanging(value);
                this.SendPropertyChanging();
                this._name = value;
                this.SendPropertyChanged("name");
                this.OnnameChanged();
		    }
		}
		
        partial void Onprincipal_idChanging(int value);
        partial void Onprincipal_idChanged();
		
		private int _principal_id;
		public int principal_id { 
		    get{
		        return _principal_id;
		    } 
		    set{
		        this.Onprincipal_idChanging(value);
                this.SendPropertyChanging();
                this._principal_id = value;
                this.SendPropertyChanged("principal_id");
                this.Onprincipal_idChanged();
		    }
		}
		
        partial void Ondiagram_idChanging(int value);
        partial void Ondiagram_idChanged();
		
		private int _diagram_id;
		public int diagram_id { 
		    get{
		        return _diagram_id;
		    } 
		    set{
		        this.Ondiagram_idChanging(value);
                this.SendPropertyChanging();
                this._diagram_id = value;
                this.SendPropertyChanged("diagram_id");
                this.Ondiagram_idChanged();
		    }
		}
		
        partial void OnversionChanging(int? value);
        partial void OnversionChanged();
		
		private int? _version;
		public int? version { 
		    get{
		        return _version;
		    } 
		    set{
		        this.OnversionChanging(value);
                this.SendPropertyChanging();
                this._version = value;
                this.SendPropertyChanged("version");
                this.OnversionChanged();
		    }
		}
		
        partial void OndefinitionChanging(byte[] value);
        partial void OndefinitionChanged();
		
		private byte[] _definition;
		public byte[] definition { 
		    get{
		        return _definition;
		    } 
		    set{
		        this.OndefinitionChanging(value);
                this.SendPropertyChanging();
                this._definition = value;
                this.SendPropertyChanged("definition");
                this.OndefinitionChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Rating table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Rating 
    /// </summary>

	public partial class Rating: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Rating(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnImageIDChanging(int value);
        partial void OnImageIDChanged();
		
		private int _ImageID;
		public int ImageID { 
		    get{
		        return _ImageID;
		    } 
		    set{
		        this.OnImageIDChanging(value);
                this.SendPropertyChanging();
                this._ImageID = value;
                this.SendPropertyChanged("ImageID");
                this.OnImageIDChanged();
		    }
		}
		
        partial void OnRePinsChanging(int? value);
        partial void OnRePinsChanged();
		
		private int? _RePins;
		public int? RePins { 
		    get{
		        return _RePins;
		    } 
		    set{
		        this.OnRePinsChanging(value);
                this.SendPropertyChanging();
                this._RePins = value;
                this.SendPropertyChanged("RePins");
                this.OnRePinsChanged();
		    }
		}
		
        partial void OnLikesChanging(int? value);
        partial void OnLikesChanged();
		
		private int? _Likes;
		public int? Likes { 
		    get{
		        return _Likes;
		    } 
		    set{
		        this.OnLikesChanging(value);
                this.SendPropertyChanging();
                this._Likes = value;
                this.SendPropertyChanged("Likes");
                this.OnLikesChanged();
		    }
		}
		
        partial void OnRatingXChanging(int? value);
        partial void OnRatingXChanged();
		
		private int? _RatingX;
		public int? RatingX { 
		    get{
		        return _RatingX;
		    } 
		    set{
		        this.OnRatingXChanging(value);
                this.SendPropertyChanging();
                this._RatingX = value;
                this.SendPropertyChanged("RatingX");
                this.OnRatingXChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Image> Images
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Images
                       where items.ID == _ImageID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Review table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Review 
    /// </summary>

	public partial class Review: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Review(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnBIMIDChanging(int value);
        partial void OnBIMIDChanged();
		
		private int _BIMID;
		public int BIMID { 
		    get{
		        return _BIMID;
		    } 
		    set{
		        this.OnBIMIDChanging(value);
                this.SendPropertyChanging();
                this._BIMID = value;
                this.SendPropertyChanged("BIMID");
                this.OnBIMIDChanged();
		    }
		}
		
        partial void OnQuestionChanging(string value);
        partial void OnQuestionChanged();
		
		private string _Question;
		public string Question { 
		    get{
		        return _Question;
		    } 
		    set{
		        this.OnQuestionChanging(value);
                this.SendPropertyChanging();
                this._Question = value;
                this.SendPropertyChanged("Question");
                this.OnQuestionChanged();
		    }
		}
		
        partial void OnAnswerChanging(string value);
        partial void OnAnswerChanged();
		
		private string _Answer;
		public string Answer { 
		    get{
		        return _Answer;
		    } 
		    set{
		        this.OnAnswerChanging(value);
                this.SendPropertyChanging();
                this._Answer = value;
                this.SendPropertyChanged("Answer");
                this.OnAnswerChanged();
		    }
		}
		
        partial void OnUserIDChanging(int? value);
        partial void OnUserIDChanged();
		
		private int? _UserID;
		public int? UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		
        partial void OnSessionIDChanging(string value);
        partial void OnSessionIDChanged();
		
		private string _SessionID;
		public string SessionID { 
		    get{
		        return _SessionID;
		    } 
		    set{
		        this.OnSessionIDChanging(value);
                this.SendPropertyChanging();
                this._SessionID = value;
                this.SendPropertyChanged("SessionID");
                this.OnSessionIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.ID == _UserID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Stores table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Store 
    /// </summary>

	public partial class Store: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Store(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnTitleChanging(string value);
        partial void OnTitleChanged();
		
		private string _Title;
		public string Title { 
		    get{
		        return _Title;
		    } 
		    set{
		        this.OnTitleChanging(value);
                this.SendPropertyChanging();
                this._Title = value;
                this.SendPropertyChanged("Title");
                this.OnTitleChanged();
		    }
		}
		
        partial void OnRelImagePathChanging(string value);
        partial void OnRelImagePathChanged();
		
		private string _RelImagePath;
		public string RelImagePath { 
		    get{
		        return _RelImagePath;
		    } 
		    set{
		        this.OnRelImagePathChanging(value);
                this.SendPropertyChanging();
                this._RelImagePath = value;
                this.SendPropertyChanged("RelImagePath");
                this.OnRelImagePathChanged();
		    }
		}
		
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnUrlChanging(string value);
        partial void OnUrlChanged();
		
		private string _Url;
		public string Url { 
		    get{
		        return _Url;
		    } 
		    set{
		        this.OnUrlChanging(value);
                this.SendPropertyChanging();
                this._Url = value;
                this.SendPropertyChanged("Url");
                this.OnUrlChanged();
		    }
		}
		
        partial void OnImage_HeightChanging(short? value);
        partial void OnImage_HeightChanged();
		
		private short? _Image_Height;
		public short? Image_Height { 
		    get{
		        return _Image_Height;
		    } 
		    set{
		        this.OnImage_HeightChanging(value);
                this.SendPropertyChanging();
                this._Image_Height = value;
                this.SendPropertyChanged("Image_Height");
                this.OnImage_HeightChanged();
		    }
		}
		
        partial void OnImage_WidthChanging(short? value);
        partial void OnImage_WidthChanged();
		
		private short? _Image_Width;
		public short? Image_Width { 
		    get{
		        return _Image_Width;
		    } 
		    set{
		        this.OnImage_WidthChanging(value);
                this.SendPropertyChanging();
                this._Image_Width = value;
                this.SendPropertyChanged("Image_Width");
                this.OnImage_WidthChanged();
		    }
		}
		
        partial void OnFNV1aChanging(decimal? value);
        partial void OnFNV1aChanged();
		
		private decimal? _FNV1a;
		public decimal? FNV1a { 
		    get{
		        return _FNV1a;
		    } 
		    set{
		        this.OnFNV1aChanging(value);
                this.SendPropertyChanging();
                this._FNV1a = value;
                this.SendPropertyChanged("FNV1a");
                this.OnFNV1aChanged();
		    }
		}
		
        partial void OnMURMUR2Changing(decimal? value);
        partial void OnMURMUR2Changed();
		
		private decimal? _MURMUR2;
		public decimal? MURMUR2 { 
		    get{
		        return _MURMUR2;
		    } 
		    set{
		        this.OnMURMUR2Changing(value);
                this.SendPropertyChanging();
                this._MURMUR2 = value;
                this.SendPropertyChanged("MURMUR2");
                this.OnMURMUR2Changed();
		    }
		}
		
        partial void OnCRC64Changing(decimal? value);
        partial void OnCRC64Changed();
		
		private decimal? _CRC64;
		public decimal? CRC64 { 
		    get{
		        return _CRC64;
		    } 
		    set{
		        this.OnCRC64Changing(value);
                this.SendPropertyChanging();
                this._CRC64 = value;
                this.SendPropertyChanged("CRC64");
                this.OnCRC64Changed();
		    }
		}
		

        #endregion

        #region Foreign Keys
        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the BoardContributor table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.BoardContributor 
    /// </summary>

	public partial class BoardContributor: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public BoardContributor(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnBoardIDChanging(int value);
        partial void OnBoardIDChanged();
		
		private int _BoardID;
		public int BoardID { 
		    get{
		        return _BoardID;
		    } 
		    set{
		        this.OnBoardIDChanging(value);
                this.SendPropertyChanging();
                this._BoardID = value;
                this.SendPropertyChanged("BoardID");
                this.OnBoardIDChanged();
		    }
		}
		
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnContributorIDChanging(int value);
        partial void OnContributorIDChanged();
		
		private int _ContributorID;
		public int ContributorID { 
		    get{
		        return _ContributorID;
		    } 
		    set{
		        this.OnContributorIDChanging(value);
                this.SendPropertyChanging();
                this._ContributorID = value;
                this.SendPropertyChanged("ContributorID");
                this.OnContributorIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.ID == _ContributorID
                       select items;
            }
        }

        public IQueryable<Board> Boards
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Boards
                       where items.ID == _BoardID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Images table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Image 
    /// </summary>

	public partial class Image: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Image(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnImage_HeightChanging(short? value);
        partial void OnImage_HeightChanged();
		
		private short? _Image_Height;
		public short? Image_Height { 
		    get{
		        return _Image_Height;
		    } 
		    set{
		        this.OnImage_HeightChanging(value);
                this.SendPropertyChanging();
                this._Image_Height = value;
                this.SendPropertyChanged("Image_Height");
                this.OnImage_HeightChanged();
		    }
		}
		
        partial void OnImage_WidthChanging(short? value);
        partial void OnImage_WidthChanged();
		
		private short? _Image_Width;
		public short? Image_Width { 
		    get{
		        return _Image_Width;
		    } 
		    set{
		        this.OnImage_WidthChanging(value);
                this.SendPropertyChanging();
                this._Image_Width = value;
                this.SendPropertyChanged("Image_Width");
                this.OnImage_WidthChanged();
		    }
		}
		
        partial void OnRelativeImage_PathChanging(string value);
        partial void OnRelativeImage_PathChanged();
		
		private string _RelativeImage_Path;
		public string RelativeImage_Path { 
		    get{
		        return _RelativeImage_Path;
		    } 
		    set{
		        this.OnRelativeImage_PathChanging(value);
                this.SendPropertyChanging();
                this._RelativeImage_Path = value;
                this.SendPropertyChanged("RelativeImage_Path");
                this.OnRelativeImage_PathChanged();
		    }
		}
		
        partial void OnDateChanging(DateTime? value);
        partial void OnDateChanged();
		
		private DateTime? _Date;
		public DateTime? Date { 
		    get{
		        return _Date;
		    } 
		    set{
		        this.OnDateChanging(value);
                this.SendPropertyChanging();
                this._Date = value;
                this.SendPropertyChanged("Date");
                this.OnDateChanged();
		    }
		}
		
        partial void OnTaggedChanging(bool? value);
        partial void OnTaggedChanged();
		
		private bool? _Tagged;
		public bool? Tagged { 
		    get{
		        return _Tagged;
		    } 
		    set{
		        this.OnTaggedChanging(value);
                this.SendPropertyChanging();
                this._Tagged = value;
                this.SendPropertyChanged("Tagged");
                this.OnTaggedChanged();
		    }
		}
		
        partial void OnCRC64Changing(decimal? value);
        partial void OnCRC64Changed();
		
		private decimal? _CRC64;
		public decimal? CRC64 { 
		    get{
		        return _CRC64;
		    } 
		    set{
		        this.OnCRC64Changing(value);
                this.SendPropertyChanging();
                this._CRC64 = value;
                this.SendPropertyChanged("CRC64");
                this.OnCRC64Changed();
		    }
		}
		
        partial void OnFNV1aChanging(decimal? value);
        partial void OnFNV1aChanged();
		
		private decimal? _FNV1a;
		public decimal? FNV1a { 
		    get{
		        return _FNV1a;
		    } 
		    set{
		        this.OnFNV1aChanging(value);
                this.SendPropertyChanging();
                this._FNV1a = value;
                this.SendPropertyChanged("FNV1a");
                this.OnFNV1aChanged();
		    }
		}
		
        partial void OnMURMUR2Changing(decimal? value);
        partial void OnMURMUR2Changed();
		
		private decimal? _MURMUR2;
		public decimal? MURMUR2 { 
		    get{
		        return _MURMUR2;
		    } 
		    set{
		        this.OnMURMUR2Changing(value);
                this.SendPropertyChanging();
                this._MURMUR2 = value;
                this.SendPropertyChanged("MURMUR2");
                this.OnMURMUR2Changed();
		    }
		}
		
        partial void OnUploadedChanging(bool? value);
        partial void OnUploadedChanged();
		
		private bool? _Uploaded;
		public bool? Uploaded { 
		    get{
		        return _Uploaded;
		    } 
		    set{
		        this.OnUploadedChanging(value);
                this.SendPropertyChanging();
                this._Uploaded = value;
                this.SendPropertyChanged("Uploaded");
                this.OnUploadedChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardsImagesMappings
                       where items.ImageID == _ID
                       select items;
            }
        }

        public IQueryable<CategoryImagesMapping> CategoryImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.CategoryImagesMappings
                       where items.ImageID == _ID
                       select items;
            }
        }

        public IQueryable<Rating> Ratings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Ratings
                       where items.ImageID == _ID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Boards table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Board 
    /// </summary>

	public partial class Board: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Board(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnCatIDChanging(int? value);
        partial void OnCatIDChanged();
		
		private int? _CatID;
		public int? CatID { 
		    get{
		        return _CatID;
		    } 
		    set{
		        this.OnCatIDChanging(value);
                this.SendPropertyChanging();
                this._CatID = value;
                this.SendPropertyChanged("CatID");
                this.OnCatIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(int? value);
        partial void OnUserIDChanged();
		
		private int? _UserID;
		public int? UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<BoardContributor> BoardContributors
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardContributors
                       where items.BoardID == _ID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardsImagesMappings
                       where items.BoardID == _ID
                       select items;
            }
        }

        public IQueryable<Category> Categories
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Categories
                       where items.ID == _CatID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the BoardsImagesMapping table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.BoardsImagesMapping 
    /// </summary>

	public partial class BoardsImagesMapping: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public BoardsImagesMapping(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnBoardIDChanging(int? value);
        partial void OnBoardIDChanged();
		
		private int? _BoardID;
		public int? BoardID { 
		    get{
		        return _BoardID;
		    } 
		    set{
		        this.OnBoardIDChanging(value);
                this.SendPropertyChanging();
                this._BoardID = value;
                this.SendPropertyChanged("BoardID");
                this.OnBoardIDChanged();
		    }
		}
		
        partial void OnImageIDChanging(int value);
        partial void OnImageIDChanged();
		
		private int _ImageID;
		public int ImageID { 
		    get{
		        return _ImageID;
		    } 
		    set{
		        this.OnImageIDChanging(value);
                this.SendPropertyChanging();
                this._ImageID = value;
                this.SendPropertyChanged("ImageID");
                this.OnImageIDChanged();
		    }
		}
		
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnImage_TitleChanging(string value);
        partial void OnImage_TitleChanged();
		
		private string _Image_Title;
		public string Image_Title { 
		    get{
		        return _Image_Title;
		    } 
		    set{
		        this.OnImage_TitleChanging(value);
                this.SendPropertyChanging();
                this._Image_Title = value;
                this.SendPropertyChanged("Image_Title");
                this.OnImage_TitleChanged();
		    }
		}
		
        partial void OnUserIDChanging(int? value);
        partial void OnUserIDChanged();
		
		private int? _UserID;
		public int? UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		
        partial void OnSourceChanging(string value);
        partial void OnSourceChanged();
		
		private string _Source;
		public string Source { 
		    get{
		        return _Source;
		    } 
		    set{
		        this.OnSourceChanging(value);
                this.SendPropertyChanging();
                this._Source = value;
                this.SendPropertyChanged("Source");
                this.OnSourceChanged();
		    }
		}
		
        partial void OnRatingChanging(int? value);
        partial void OnRatingChanged();
		
		private int? _Rating;
		public int? Rating { 
		    get{
		        return _Rating;
		    } 
		    set{
		        this.OnRatingChanging(value);
                this.SendPropertyChanging();
                this._Rating = value;
                this.SendPropertyChanged("Rating");
                this.OnRatingChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<Board> Boards
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Boards
                       where items.ID == _BoardID
                       select items;
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Comments
                       where items.BoardsImagesMappingID == _ID
                       select items;
            }
        }

        public IQueryable<Like> Likes
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Likes
                       where items.BoardsImagesMappingID == _ID
                       select items;
            }
        }

        public IQueryable<Image> Images
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Images
                       where items.ID == _ImageID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Comments table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Comment 
    /// </summary>

	public partial class Comment: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Comment(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnCommentsChanging(string value);
        partial void OnCommentsChanged();
		
		private string _Comments;
		public string Comments { 
		    get{
		        return _Comments;
		    } 
		    set{
		        this.OnCommentsChanging(value);
                this.SendPropertyChanging();
                this._Comments = value;
                this.SendPropertyChanged("Comments");
                this.OnCommentsChanged();
		    }
		}
		
        partial void OnBoardsImagesMappingIDChanging(int value);
        partial void OnBoardsImagesMappingIDChanged();
		
		private int _BoardsImagesMappingID;
		public int BoardsImagesMappingID { 
		    get{
		        return _BoardsImagesMappingID;
		    } 
		    set{
		        this.OnBoardsImagesMappingIDChanging(value);
                this.SendPropertyChanging();
                this._BoardsImagesMappingID = value;
                this.SendPropertyChanged("BoardsImagesMappingID");
                this.OnBoardsImagesMappingIDChanged();
		    }
		}
		
        partial void OnUserIDChanging(int value);
        partial void OnUserIDChanged();
		
		private int _UserID;
		public int UserID { 
		    get{
		        return _UserID;
		    } 
		    set{
		        this.OnUserIDChanging(value);
                this.SendPropertyChanging();
                this._UserID = value;
                this.SendPropertyChanged("UserID");
                this.OnUserIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.BoardsImagesMappings
                       where items.ID == _BoardsImagesMappingID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Category table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Category 
    /// </summary>

	public partial class Category: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Category(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void OnIDChanging(int value);
        partial void OnIDChanged();
		
		private int _ID;
		public int ID { 
		    get{
		        return _ID;
		    } 
		    set{
		        this.OnIDChanging(value);
                this.SendPropertyChanging();
                this._ID = value;
                this.SendPropertyChanged("ID");
                this.OnIDChanged();
		    }
		}
		
        partial void OnNameChanging(string value);
        partial void OnNameChanged();
		
		private string _Name;
		public string Name { 
		    get{
		        return _Name;
		    } 
		    set{
		        this.OnNameChanging(value);
                this.SendPropertyChanging();
                this._Name = value;
                this.SendPropertyChanged("Name");
                this.OnNameChanged();
		    }
		}
		
        partial void OnParentIDChanging(int? value);
        partial void OnParentIDChanged();
		
		private int? _ParentID;
		public int? ParentID { 
		    get{
		        return _ParentID;
		    } 
		    set{
		        this.OnParentIDChanging(value);
                this.SendPropertyChanging();
                this._ParentID = value;
                this.SendPropertyChanged("ParentID");
                this.OnParentIDChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<Board> Boards
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.Boards
                       where items.CatID == _ID
                       select items;
            }
        }

        public IQueryable<CategoryImagesMapping> CategoryImagesMappings
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.CategoryImagesMappings
                       where items.CategoryID == _ID
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
    
    
    /// <summary>
    /// A class which represents the Facebook table in the PindexProd Database.
    /// This class is queryable through PindexProdDB.Facebook 
    /// </summary>

	public partial class Facebook: INotifyPropertyChanging, INotifyPropertyChanged
	{
        partial void OnLoaded();
        partial void OnValidate(System.Data.Linq.ChangeAction action);
        partial void OnCreated();
	    
	    public Facebook(){
	        OnCreated();
	    }
	    
	    #region Properties
	    
        partial void Onfirst_nameChanging(string value);
        partial void Onfirst_nameChanged();
		
		private string _first_name;
		public string first_name { 
		    get{
		        return _first_name;
		    } 
		    set{
		        this.Onfirst_nameChanging(value);
                this.SendPropertyChanging();
                this._first_name = value;
                this.SendPropertyChanged("first_name");
                this.Onfirst_nameChanged();
		    }
		}
		
        partial void OngenderChanging(string value);
        partial void OngenderChanged();
		
		private string _gender;
		public string gender { 
		    get{
		        return _gender;
		    } 
		    set{
		        this.OngenderChanging(value);
                this.SendPropertyChanging();
                this._gender = value;
                this.SendPropertyChanged("gender");
                this.OngenderChanged();
		    }
		}
		
        partial void OnidChanging(decimal value);
        partial void OnidChanged();
		
		private decimal _id;
		public decimal id { 
		    get{
		        return _id;
		    } 
		    set{
		        this.OnidChanging(value);
                this.SendPropertyChanging();
                this._id = value;
                this.SendPropertyChanged("id");
                this.OnidChanged();
		    }
		}
		
        partial void Onlast_nameChanging(string value);
        partial void Onlast_nameChanged();
		
		private string _last_name;
		public string last_name { 
		    get{
		        return _last_name;
		    } 
		    set{
		        this.Onlast_nameChanging(value);
                this.SendPropertyChanging();
                this._last_name = value;
                this.SendPropertyChanged("last_name");
                this.Onlast_nameChanged();
		    }
		}
		
        partial void OnlinkChanging(string value);
        partial void OnlinkChanged();
		
		private string _link;
		public string link { 
		    get{
		        return _link;
		    } 
		    set{
		        this.OnlinkChanging(value);
                this.SendPropertyChanging();
                this._link = value;
                this.SendPropertyChanged("link");
                this.OnlinkChanged();
		    }
		}
		
        partial void OnlocaleChanging(string value);
        partial void OnlocaleChanged();
		
		private string _locale;
		public string locale { 
		    get{
		        return _locale;
		    } 
		    set{
		        this.OnlocaleChanging(value);
                this.SendPropertyChanging();
                this._locale = value;
                this.SendPropertyChanged("locale");
                this.OnlocaleChanged();
		    }
		}
		
        partial void OnnameChanging(string value);
        partial void OnnameChanged();
		
		private string _name;
		public string name { 
		    get{
		        return _name;
		    } 
		    set{
		        this.OnnameChanging(value);
                this.SendPropertyChanging();
                this._name = value;
                this.SendPropertyChanged("name");
                this.OnnameChanged();
		    }
		}
		
        partial void OntimezoneChanging(double? value);
        partial void OntimezoneChanged();
		
		private double? _timezone;
		public double? timezone { 
		    get{
		        return _timezone;
		    } 
		    set{
		        this.OntimezoneChanging(value);
                this.SendPropertyChanging();
                this._timezone = value;
                this.SendPropertyChanged("timezone");
                this.OntimezoneChanged();
		    }
		}
		
        partial void Onupdated_timeChanging(DateTime? value);
        partial void Onupdated_timeChanged();
		
		private DateTime? _updated_time;
		public DateTime? updated_time { 
		    get{
		        return _updated_time;
		    } 
		    set{
		        this.Onupdated_timeChanging(value);
                this.SendPropertyChanging();
                this._updated_time = value;
                this.SendPropertyChanged("updated_time");
                this.Onupdated_timeChanged();
		    }
		}
		

        #endregion

        #region Foreign Keys
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                  var db=new SubSonic.POCOS1.PindexProdDB();
                  return from items in db.AppUsers
                       where items.facebookid == _id
                       select items;
            }
        }

        #endregion


        private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
        public event PropertyChangingEventHandler PropertyChanging;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void SendPropertyChanging()
        {
            var handler = PropertyChanging;
            if (handler != null)
               handler(this, emptyChangingEventArgs);
        }

        protected virtual void SendPropertyChanged(String propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

	}
	
}