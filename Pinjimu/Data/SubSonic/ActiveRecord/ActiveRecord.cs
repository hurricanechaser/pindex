


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SubSonic.DataProviders;
using SubSonic.Extensions;
using System.Linq.Expressions;
using SubSonic.Schema;
using System.Collections;
using SubSonic;
using SubSonic.Repository;
using System.ComponentModel;
using System.Data.Common;

namespace SubSonic.POCOS
{
    
    
    /// <summary>
    /// A class which represents the User table in the Pinjimu Database.
    /// </summary>
    public partial class User: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<User> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<User>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<User> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(User item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                User item=new User();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<User> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public User(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                User.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<User>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public User(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public User(Expression<Func<User, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<User> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<User> _repo;
            
            if(db.TestMode){
                User.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<User>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<User> GetRepo(){
            return GetRepo("","");
        }
        
        public static User SingleOrDefault(Expression<Func<User, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            User single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static User SingleOrDefault(Expression<Func<User, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            User single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<User, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<User, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<User> Find(Expression<Func<User, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<User> Find(Expression<Func<User, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<User> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<User> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<User> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<User> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<User> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<User> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(User)){
                User compare=(User)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Name";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<UserBatchAssigned> UserBatchAssigneds
        {
            get
            {
                
                  var repo=SubSonic.POCOS.UserBatchAssigned.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name!=value){
                    _Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if(_Password!=value){
                    _Password=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Password");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<User, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Stores table in the Pinjimu Database.
    /// </summary>
    public partial class Store: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Store> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Store>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Store> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Store item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Store item=new Store();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Store> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Store(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Store.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Store>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Store(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Store(Expression<Func<Store, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Store> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Store> _repo;
            
            if(db.TestMode){
                Store.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Store>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Store> GetRepo(){
            return GetRepo("","");
        }
        
        public static Store SingleOrDefault(Expression<Func<Store, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Store single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Store SingleOrDefault(Expression<Func<Store, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Store single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Store, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Store, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Store> Find(Expression<Func<Store, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Store> Find(Expression<Func<Store, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Store> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Store> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Store> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Store> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Store> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Store> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Title.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Store)){
                Store compare=(Store)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Title.ToString();
                    }

        public string DescriptorColumn() {
            return "Title";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Title";
        }
        
        #region ' Foreign Keys '
        #endregion
        

        string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if(_Title!=value){
                    _Title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _RelImagePath;
        public string RelImagePath
        {
            get { return _RelImagePath; }
            set
            {
                if(_RelImagePath!=value){
                    _RelImagePath=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RelImagePath");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Url;
        public string Url
        {
            get { return _Url; }
            set
            {
                if(_Url!=value){
                    _Url=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Url");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _Image_Height;
        public short? Image_Height
        {
            get { return _Image_Height; }
            set
            {
                if(_Image_Height!=value){
                    _Image_Height=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Height");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _Image_Width;
        public short? Image_Width
        {
            get { return _Image_Width; }
            set
            {
                if(_Image_Width!=value){
                    _Image_Width=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Width");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _FNV1a;
        public decimal? FNV1a
        {
            get { return _FNV1a; }
            set
            {
                if(_FNV1a!=value){
                    _FNV1a=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FNV1a");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _MURMUR2;
        public decimal? MURMUR2
        {
            get { return _MURMUR2; }
            set
            {
                if(_MURMUR2!=value){
                    _MURMUR2=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MURMUR2");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _CRC64;
        public decimal? CRC64
        {
            get { return _CRC64; }
            set
            {
                if(_CRC64!=value){
                    _CRC64=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CRC64");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Store, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Images table in the Pinjimu Database.
    /// </summary>
    public partial class Image: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Image> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Image>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Image> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Image item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Image item=new Image();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Image> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Image(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Image.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Image>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Image(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Image(Expression<Func<Image, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Image> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Image> _repo;
            
            if(db.TestMode){
                Image.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Image>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Image> GetRepo(){
            return GetRepo("","");
        }
        
        public static Image SingleOrDefault(Expression<Func<Image, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Image single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Image SingleOrDefault(Expression<Func<Image, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Image single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Image, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Image, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Image> Find(Expression<Func<Image, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Image> Find(Expression<Func<Image, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Image> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Image> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Image> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Image> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Image> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Image> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.RelativeImage_Path.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Image)){
                Image compare=(Image)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.RelativeImage_Path.ToString();
                    }

        public string DescriptorColumn() {
            return "RelativeImage_Path";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "RelativeImage_Path";
        }
        
        #region ' Foreign Keys '
        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardsImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.ImageID == _ID
                       select items;
            }
        }

        public IQueryable<CategoryImagesMapping> CategoryImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.CategoryImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.ImageID == _ID
                       select items;
            }
        }

        public IQueryable<Rating> Ratings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Rating.GetRepo();
                  return from items in repo.GetAll()
                       where items.ImageID == _ID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _Image_Height;
        public short? Image_Height
        {
            get { return _Image_Height; }
            set
            {
                if(_Image_Height!=value){
                    _Image_Height=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Height");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _Image_Width;
        public short? Image_Width
        {
            get { return _Image_Width; }
            set
            {
                if(_Image_Width!=value){
                    _Image_Width=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Width");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _RelativeImage_Path;
        public string RelativeImage_Path
        {
            get { return _RelativeImage_Path; }
            set
            {
                if(_RelativeImage_Path!=value){
                    _RelativeImage_Path=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RelativeImage_Path");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime? _Date;
        public DateTime? Date
        {
            get { return _Date; }
            set
            {
                if(_Date!=value){
                    _Date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool? _Tagged;
        public bool? Tagged
        {
            get { return _Tagged; }
            set
            {
                if(_Tagged!=value){
                    _Tagged=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Tagged");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _CRC64;
        public decimal? CRC64
        {
            get { return _CRC64; }
            set
            {
                if(_CRC64!=value){
                    _CRC64=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CRC64");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _FNV1a;
        public decimal? FNV1a
        {
            get { return _FNV1a; }
            set
            {
                if(_FNV1a!=value){
                    _FNV1a=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FNV1a");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _MURMUR2;
        public decimal? MURMUR2
        {
            get { return _MURMUR2; }
            set
            {
                if(_MURMUR2!=value){
                    _MURMUR2=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MURMUR2");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool? _Uploaded;
        public bool? Uploaded
        {
            get { return _Uploaded; }
            set
            {
                if(_Uploaded!=value){
                    _Uploaded=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Uploaded");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        bool? _Verified;
        public bool? Verified
        {
            get { return _Verified; }
            set
            {
                if(_Verified!=value){
                    _Verified=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Verified");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Image, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Facebook table in the Pinjimu Database.
    /// </summary>
    public partial class Facebook: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Facebook> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Facebook>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Facebook> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Facebook item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Facebook item=new Facebook();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Facebook> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Facebook(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Facebook.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Facebook>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Facebook(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Facebook(Expression<Func<Facebook, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Facebook> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Facebook> _repo;
            
            if(db.TestMode){
                Facebook.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Facebook>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Facebook> GetRepo(){
            return GetRepo("","");
        }
        
        public static Facebook SingleOrDefault(Expression<Func<Facebook, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Facebook single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Facebook SingleOrDefault(Expression<Func<Facebook, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Facebook single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Facebook, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Facebook, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Facebook> Find(Expression<Func<Facebook, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Facebook> Find(Expression<Func<Facebook, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Facebook> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Facebook> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Facebook> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Facebook> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Facebook> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Facebook> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "id";
        }

        public object KeyValue()
        {
            return this.id;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<decimal>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.first_name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Facebook)){
                Facebook compare=(Facebook)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        public string DescriptorValue()
        {
                            return this.first_name.ToString();
                    }

        public string DescriptorColumn() {
            return "first_name";
        }
        public static string GetKeyColumn()
        {
            return "id";
        }        
        public static string GetDescriptorColumn()
        {
            return "first_name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.facebookid == _id
                       select items;
            }
        }

        #endregion
        

        string _first_name;
        public string first_name
        {
            get { return _first_name; }
            set
            {
                if(_first_name!=value){
                    _first_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="first_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _gender;
        public string gender
        {
            get { return _gender; }
            set
            {
                if(_gender!=value){
                    _gender=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="gender");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal _id;
        public decimal id
        {
            get { return _id; }
            set
            {
                if(_id!=value){
                    _id=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="id");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _last_name;
        public string last_name
        {
            get { return _last_name; }
            set
            {
                if(_last_name!=value){
                    _last_name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="last_name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _link;
        public string link
        {
            get { return _link; }
            set
            {
                if(_link!=value){
                    _link=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="link");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _locale;
        public string locale
        {
            get { return _locale; }
            set
            {
                if(_locale!=value){
                    _locale=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="locale");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _name;
        public string name
        {
            get { return _name; }
            set
            {
                if(_name!=value){
                    _name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        double? _timezone;
        public double? timezone
        {
            get { return _timezone; }
            set
            {
                if(_timezone!=value){
                    _timezone=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="timezone");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime? _updated_time;
        public DateTime? updated_time
        {
            get { return _updated_time; }
            set
            {
                if(_updated_time!=value){
                    _updated_time=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="updated_time");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Facebook, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the UserBatchAssigned table in the Pinjimu Database.
    /// </summary>
    public partial class UserBatchAssigned: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<UserBatchAssigned> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<UserBatchAssigned>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<UserBatchAssigned> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(UserBatchAssigned item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                UserBatchAssigned item=new UserBatchAssigned();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<UserBatchAssigned> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public UserBatchAssigned(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                UserBatchAssigned.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<UserBatchAssigned>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public UserBatchAssigned(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public UserBatchAssigned(Expression<Func<UserBatchAssigned, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<UserBatchAssigned> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<UserBatchAssigned> _repo;
            
            if(db.TestMode){
                UserBatchAssigned.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<UserBatchAssigned>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<UserBatchAssigned> GetRepo(){
            return GetRepo("","");
        }
        
        public static UserBatchAssigned SingleOrDefault(Expression<Func<UserBatchAssigned, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            UserBatchAssigned single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static UserBatchAssigned SingleOrDefault(Expression<Func<UserBatchAssigned, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            UserBatchAssigned single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<UserBatchAssigned, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<UserBatchAssigned, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<UserBatchAssigned> Find(Expression<Func<UserBatchAssigned, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<UserBatchAssigned> Find(Expression<Func<UserBatchAssigned, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<UserBatchAssigned> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<UserBatchAssigned> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<UserBatchAssigned> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<UserBatchAssigned> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<UserBatchAssigned> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<UserBatchAssigned> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.UserID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(UserBatchAssigned)){
                UserBatchAssigned compare=(UserBatchAssigned)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.UserID.ToString();
                    }

        public string DescriptorColumn() {
            return "UserID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "UserID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<User> Users
        {
            get
            {
                
                  var repo=SubSonic.POCOS.User.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BatchStart;
        public int BatchStart
        {
            get { return _BatchStart; }
            set
            {
                if(_BatchStart!=value){
                    _BatchStart=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BatchStart");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BatchEnd;
        public int BatchEnd
        {
            get { return _BatchEnd; }
            set
            {
                if(_BatchEnd!=value){
                    _BatchEnd=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BatchEnd");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<UserBatchAssigned, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the CategoryImagesMapping table in the Pinjimu Database.
    /// </summary>
    public partial class CategoryImagesMapping: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<CategoryImagesMapping> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<CategoryImagesMapping>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<CategoryImagesMapping> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(CategoryImagesMapping item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                CategoryImagesMapping item=new CategoryImagesMapping();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<CategoryImagesMapping> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public CategoryImagesMapping(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                CategoryImagesMapping.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CategoryImagesMapping>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public CategoryImagesMapping(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public CategoryImagesMapping(Expression<Func<CategoryImagesMapping, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<CategoryImagesMapping> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<CategoryImagesMapping> _repo;
            
            if(db.TestMode){
                CategoryImagesMapping.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<CategoryImagesMapping>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<CategoryImagesMapping> GetRepo(){
            return GetRepo("","");
        }
        
        public static CategoryImagesMapping SingleOrDefault(Expression<Func<CategoryImagesMapping, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            CategoryImagesMapping single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static CategoryImagesMapping SingleOrDefault(Expression<Func<CategoryImagesMapping, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            CategoryImagesMapping single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<CategoryImagesMapping, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<CategoryImagesMapping, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<CategoryImagesMapping> Find(Expression<Func<CategoryImagesMapping, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<CategoryImagesMapping> Find(Expression<Func<CategoryImagesMapping, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<CategoryImagesMapping> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<CategoryImagesMapping> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<CategoryImagesMapping> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<CategoryImagesMapping> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<CategoryImagesMapping> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<CategoryImagesMapping> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.ImageID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(CategoryImagesMapping)){
                CategoryImagesMapping compare=(CategoryImagesMapping)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.ImageID.ToString();
                    }

        public string DescriptorColumn() {
            return "ImageID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "ImageID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<Category> Categories
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Category.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _CategoryID
                       select items;
            }
        }

        public IQueryable<Image> Images
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Image.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _ImageID
                       select items;
            }
        }

        #endregion
        

        int _CategoryID;
        public int CategoryID
        {
            get { return _CategoryID; }
            set
            {
                if(_CategoryID!=value){
                    _CategoryID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CategoryID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ImageID;
        public int ImageID
        {
            get { return _ImageID; }
            set
            {
                if(_ImageID!=value){
                    _ImageID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ImageID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _UserID;
        public int? UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<CategoryImagesMapping, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the AppUsers table in the Pinjimu Database.
    /// </summary>
    public partial class AppUser: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<AppUser> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<AppUser>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<AppUser> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(AppUser item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                AppUser item=new AppUser();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<AppUser> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public AppUser(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                AppUser.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<AppUser>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public AppUser(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public AppUser(Expression<Func<AppUser, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<AppUser> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<AppUser> _repo;
            
            if(db.TestMode){
                AppUser.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<AppUser>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<AppUser> GetRepo(){
            return GetRepo("","");
        }
        
        public static AppUser SingleOrDefault(Expression<Func<AppUser, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            AppUser single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static AppUser SingleOrDefault(Expression<Func<AppUser, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            AppUser single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<AppUser, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<AppUser, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<AppUser> Find(Expression<Func<AppUser, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<AppUser> Find(Expression<Func<AppUser, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<AppUser> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<AppUser> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<AppUser> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<AppUser> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<AppUser> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<AppUser> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(AppUser)){
                AppUser compare=(AppUser)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Name";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<PointsHistory> PointsHistories
        {
            get
            {
                
                  var repo=SubSonic.POCOS.PointsHistory.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<PrizeHistory> PrizeHistories
        {
            get
            {
                
                  var repo=SubSonic.POCOS.PrizeHistory.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<BoardContributor> BoardContributors
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardContributor.GetRepo();
                  return from items in repo.GetAll()
                       where items.ContributorID == _ID
                       select items;
            }
        }

        public IQueryable<Board> Boards
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Board.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardsImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Comment.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<FollowingUser> FollowingUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.FollowingUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<FollowingUser> FollowingUsers7
        {
            get
            {
                
                  var repo=SubSonic.POCOS.FollowingUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.FollowingID == _ID
                       select items;
            }
        }

        public IQueryable<Like> Likes
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Like.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Review> Reviews
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Review.GetRepo();
                  return from items in repo.GetAll()
                       where items.UserID == _ID
                       select items;
            }
        }

        public IQueryable<Facebook> Facebooks
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Facebook.GetRepo();
                  return from items in repo.GetAll()
                       where items.id == _facebookid
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name!=value){
                    _Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                if(_Password!=value){
                    _Password=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Password");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Email;
        public string Email
        {
            get { return _Email; }
            set
            {
                if(_Email!=value){
                    _Email=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Email");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Avatar;
        public string Avatar
        {
            get { return _Avatar; }
            set
            {
                if(_Avatar!=value){
                    _Avatar=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Avatar");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set
            {
                if(_FirstName!=value){
                    _FirstName=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FirstName");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _About;
        public string About
        {
            get { return _About; }
            set
            {
                if(_About!=value){
                    _About=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="About");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Location;
        public string Location
        {
            get { return _Location; }
            set
            {
                if(_Location!=value){
                    _Location=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Location");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Website;
        public string Website
        {
            get { return _Website; }
            set
            {
                if(_Website!=value){
                    _Website=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Website");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Invite;
        public string Invite
        {
            get { return _Invite; }
            set
            {
                if(_Invite!=value){
                    _Invite=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Invite");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _facebookid;
        public decimal? facebookid
        {
            get { return _facebookid; }
            set
            {
                if(_facebookid!=value){
                    _facebookid=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="facebookid");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _Points;
        public int? Points
        {
            get { return _Points; }
            set
            {
                if(_Points!=value){
                    _Points=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Points");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _Create_date;
        public DateTime Create_date
        {
            get { return _Create_date; }
            set
            {
                if(_Create_date!=value){
                    _Create_date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Create_date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Speciality;
        public string Speciality
        {
            get { return _Speciality; }
            set
            {
                if(_Speciality!=value){
                    _Speciality=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Speciality");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<AppUser, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Review table in the Pinjimu Database.
    /// </summary>
    public partial class Review: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Review> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Review>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Review> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Review item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Review item=new Review();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Review> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Review(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Review.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Review>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Review(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Review(Expression<Func<Review, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Review> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Review> _repo;
            
            if(db.TestMode){
                Review.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Review>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Review> GetRepo(){
            return GetRepo("","");
        }
        
        public static Review SingleOrDefault(Expression<Func<Review, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Review single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Review SingleOrDefault(Expression<Func<Review, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Review single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Review, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Review, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Review> Find(Expression<Func<Review, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Review> Find(Expression<Func<Review, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Review> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Review> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Review> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Review> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Review> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Review> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Question.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Review)){
                Review compare=(Review)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Question.ToString();
                    }

        public string DescriptorColumn() {
            return "Question";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Question";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BIMID;
        public int BIMID
        {
            get { return _BIMID; }
            set
            {
                if(_BIMID!=value){
                    _BIMID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BIMID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Question;
        public string Question
        {
            get { return _Question; }
            set
            {
                if(_Question!=value){
                    _Question=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Question");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Answer;
        public string Answer
        {
            get { return _Answer; }
            set
            {
                if(_Answer!=value){
                    _Answer=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Answer");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _UserID;
        public int? UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _SessionID;
        public string SessionID
        {
            get { return _SessionID; }
            set
            {
                if(_SessionID!=value){
                    _SessionID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="SessionID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Review, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the BoardContributor table in the Pinjimu Database.
    /// </summary>
    public partial class BoardContributor: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<BoardContributor> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<BoardContributor>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<BoardContributor> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(BoardContributor item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                BoardContributor item=new BoardContributor();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<BoardContributor> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public BoardContributor(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                BoardContributor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<BoardContributor>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public BoardContributor(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public BoardContributor(Expression<Func<BoardContributor, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<BoardContributor> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<BoardContributor> _repo;
            
            if(db.TestMode){
                BoardContributor.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<BoardContributor>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<BoardContributor> GetRepo(){
            return GetRepo("","");
        }
        
        public static BoardContributor SingleOrDefault(Expression<Func<BoardContributor, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            BoardContributor single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static BoardContributor SingleOrDefault(Expression<Func<BoardContributor, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            BoardContributor single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<BoardContributor, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<BoardContributor, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<BoardContributor> Find(Expression<Func<BoardContributor, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<BoardContributor> Find(Expression<Func<BoardContributor, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<BoardContributor> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<BoardContributor> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<BoardContributor> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<BoardContributor> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<BoardContributor> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<BoardContributor> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.ID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(BoardContributor)){
                BoardContributor compare=(BoardContributor)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.ID.ToString();
                    }

        public string DescriptorColumn() {
            return "ID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "ID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _ContributorID
                       select items;
            }
        }

        public IQueryable<Board> Boards
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Board.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _BoardID
                       select items;
            }
        }

        #endregion
        

        int _BoardID;
        public int BoardID
        {
            get { return _BoardID; }
            set
            {
                if(_BoardID!=value){
                    _BoardID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BoardID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ContributorID;
        public int ContributorID
        {
            get { return _ContributorID; }
            set
            {
                if(_ContributorID!=value){
                    _ContributorID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ContributorID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<BoardContributor, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the BoardsImagesMapping table in the Pinjimu Database.
    /// </summary>
    public partial class BoardsImagesMapping: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<BoardsImagesMapping> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<BoardsImagesMapping>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<BoardsImagesMapping> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(BoardsImagesMapping item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                BoardsImagesMapping item=new BoardsImagesMapping();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<BoardsImagesMapping> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public BoardsImagesMapping(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                BoardsImagesMapping.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<BoardsImagesMapping>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public BoardsImagesMapping(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public BoardsImagesMapping(Expression<Func<BoardsImagesMapping, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<BoardsImagesMapping> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<BoardsImagesMapping> _repo;
            
            if(db.TestMode){
                BoardsImagesMapping.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<BoardsImagesMapping>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<BoardsImagesMapping> GetRepo(){
            return GetRepo("","");
        }
        
        public static BoardsImagesMapping SingleOrDefault(Expression<Func<BoardsImagesMapping, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            BoardsImagesMapping single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static BoardsImagesMapping SingleOrDefault(Expression<Func<BoardsImagesMapping, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            BoardsImagesMapping single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<BoardsImagesMapping, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<BoardsImagesMapping, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<BoardsImagesMapping> Find(Expression<Func<BoardsImagesMapping, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<BoardsImagesMapping> Find(Expression<Func<BoardsImagesMapping, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<BoardsImagesMapping> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<BoardsImagesMapping> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<BoardsImagesMapping> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<BoardsImagesMapping> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<BoardsImagesMapping> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<BoardsImagesMapping> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Image_Title.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(BoardsImagesMapping)){
                BoardsImagesMapping compare=(BoardsImagesMapping)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Image_Title.ToString();
                    }

        public string DescriptorColumn() {
            return "Image_Title";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Image_Title";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<Board> Boards
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Board.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _BoardID
                       select items;
            }
        }

        public IQueryable<Comment> Comments
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Comment.GetRepo();
                  return from items in repo.GetAll()
                       where items.BoardsImagesMappingID == _ID
                       select items;
            }
        }

        public IQueryable<Like> Likes
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Like.GetRepo();
                  return from items in repo.GetAll()
                       where items.BoardsImagesMappingID == _ID
                       select items;
            }
        }

        public IQueryable<Image> Images
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Image.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _ImageID
                       select items;
            }
        }

        #endregion
        

        int? _BoardID;
        public int? BoardID
        {
            get { return _BoardID; }
            set
            {
                if(_BoardID!=value){
                    _BoardID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BoardID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ImageID;
        public int ImageID
        {
            get { return _ImageID; }
            set
            {
                if(_ImageID!=value){
                    _ImageID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ImageID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Image_Title;
        public string Image_Title
        {
            get { return _Image_Title; }
            set
            {
                if(_Image_Title!=value){
                    _Image_Title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _UserID;
        public int? UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Source;
        public string Source
        {
            get { return _Source; }
            set
            {
                if(_Source!=value){
                    _Source=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Source");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _Rating;
        public int? Rating
        {
            get { return _Rating; }
            set
            {
                if(_Rating!=value){
                    _Rating=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Rating");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<BoardsImagesMapping, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Likes table in the Pinjimu Database.
    /// </summary>
    public partial class Like: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Like> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Like>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Like> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Like item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Like item=new Like();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Like> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Like(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Like.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Like>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Like(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Like(Expression<Func<Like, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Like> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Like> _repo;
            
            if(db.TestMode){
                Like.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Like>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Like> GetRepo(){
            return GetRepo("","");
        }
        
        public static Like SingleOrDefault(Expression<Func<Like, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Like single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Like SingleOrDefault(Expression<Func<Like, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Like single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Like, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Like, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Like> Find(Expression<Func<Like, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Like> Find(Expression<Func<Like, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Like> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Like> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Like> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Like> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Like> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Like> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.BoardsImagesMappingID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Like)){
                Like compare=(Like)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.BoardsImagesMappingID.ToString();
                    }

        public string DescriptorColumn() {
            return "BoardsImagesMappingID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "BoardsImagesMappingID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardsImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _BoardsImagesMappingID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BoardsImagesMappingID;
        public int BoardsImagesMappingID
        {
            get { return _BoardsImagesMappingID; }
            set
            {
                if(_BoardsImagesMappingID!=value){
                    _BoardsImagesMappingID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BoardsImagesMappingID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Like, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Prize table in the Pinjimu Database.
    /// </summary>
    public partial class Prize: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Prize> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Prize>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Prize> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Prize item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Prize item=new Prize();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Prize> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Prize(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Prize.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Prize>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Prize(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Prize(Expression<Func<Prize, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Prize> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Prize> _repo;
            
            if(db.TestMode){
                Prize.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Prize>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Prize> GetRepo(){
            return GetRepo("","");
        }
        
        public static Prize SingleOrDefault(Expression<Func<Prize, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Prize single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Prize SingleOrDefault(Expression<Func<Prize, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Prize single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Prize, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Prize, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Prize> Find(Expression<Func<Prize, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Prize> Find(Expression<Func<Prize, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Prize> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Prize> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Prize> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Prize> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Prize> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Prize> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Prize_Name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Prize)){
                Prize compare=(Prize)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Prize_Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Prize_Name";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Prize_Name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<PrizeHistory> PrizeHistories
        {
            get
            {
                
                  var repo=SubSonic.POCOS.PrizeHistory.GetRepo();
                  return from items in repo.GetAll()
                       where items.PrizeID == _ID
                       select items;
            }
        }

        public IQueryable<Roulette> Roulettes
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Roulette.GetRepo();
                  return from items in repo.GetAll()
                       where items.PrizeID == _ID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Prize_Name;
        public string Prize_Name
        {
            get { return _Prize_Name; }
            set
            {
                if(_Prize_Name!=value){
                    _Prize_Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Prize_Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _User_Alert;
        public string User_Alert
        {
            get { return _User_Alert; }
            set
            {
                if(_User_Alert!=value){
                    _User_Alert=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="User_Alert");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Prize, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Roulette table in the Pinjimu Database.
    /// </summary>
    public partial class Roulette: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Roulette> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Roulette>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Roulette> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Roulette item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Roulette item=new Roulette();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Roulette> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Roulette(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Roulette.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Roulette>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Roulette(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Roulette(Expression<Func<Roulette, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Roulette> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Roulette> _repo;
            
            if(db.TestMode){
                Roulette.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Roulette>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Roulette> GetRepo(){
            return GetRepo("","");
        }
        
        public static Roulette SingleOrDefault(Expression<Func<Roulette, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Roulette single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Roulette SingleOrDefault(Expression<Func<Roulette, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Roulette single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Roulette, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Roulette, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Roulette> Find(Expression<Func<Roulette, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Roulette> Find(Expression<Func<Roulette, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Roulette> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Roulette> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Roulette> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Roulette> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Roulette> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Roulette> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Start_Angle.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Roulette)){
                Roulette compare=(Roulette)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Start_Angle.ToString();
                    }

        public string DescriptorColumn() {
            return "Start_Angle";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Start_Angle";
        }
        
        #region ' Foreign Keys '
        public IQueryable<Prize> Prizes
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Prize.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _PrizeID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _Start_Angle;
        public int Start_Angle
        {
            get { return _Start_Angle; }
            set
            {
                if(_Start_Angle!=value){
                    _Start_Angle=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Start_Angle");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _End_Angle;
        public int End_Angle
        {
            get { return _End_Angle; }
            set
            {
                if(_End_Angle!=value){
                    _End_Angle=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="End_Angle");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _PrizeID;
        public int PrizeID
        {
            get { return _PrizeID; }
            set
            {
                if(_PrizeID!=value){
                    _PrizeID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PrizeID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Roulette, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the PrizeHistory table in the Pinjimu Database.
    /// </summary>
    public partial class PrizeHistory: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<PrizeHistory> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<PrizeHistory>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<PrizeHistory> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(PrizeHistory item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                PrizeHistory item=new PrizeHistory();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<PrizeHistory> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public PrizeHistory(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                PrizeHistory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<PrizeHistory>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public PrizeHistory(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public PrizeHistory(Expression<Func<PrizeHistory, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<PrizeHistory> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<PrizeHistory> _repo;
            
            if(db.TestMode){
                PrizeHistory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<PrizeHistory>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<PrizeHistory> GetRepo(){
            return GetRepo("","");
        }
        
        public static PrizeHistory SingleOrDefault(Expression<Func<PrizeHistory, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            PrizeHistory single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static PrizeHistory SingleOrDefault(Expression<Func<PrizeHistory, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            PrizeHistory single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<PrizeHistory, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<PrizeHistory, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<PrizeHistory> Find(Expression<Func<PrizeHistory, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<PrizeHistory> Find(Expression<Func<PrizeHistory, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<PrizeHistory> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<PrizeHistory> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<PrizeHistory> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<PrizeHistory> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<PrizeHistory> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<PrizeHistory> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.PrizeID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(PrizeHistory)){
                PrizeHistory compare=(PrizeHistory)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.PrizeID.ToString();
                    }

        public string DescriptorColumn() {
            return "PrizeID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "PrizeID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<Prize> Prizes
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Prize.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _PrizeID
                       select items;
            }
        }

        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _PrizeID;
        public int PrizeID
        {
            get { return _PrizeID; }
            set
            {
                if(_PrizeID!=value){
                    _PrizeID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PrizeID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _Create_date;
        public DateTime Create_date
        {
            get { return _Create_date; }
            set
            {
                if(_Create_date!=value){
                    _Create_date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Create_date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<PrizeHistory, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the FollowingUser table in the Pinjimu Database.
    /// </summary>
    public partial class FollowingUser: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<FollowingUser> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<FollowingUser>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<FollowingUser> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(FollowingUser item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                FollowingUser item=new FollowingUser();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<FollowingUser> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public FollowingUser(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                FollowingUser.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<FollowingUser>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public FollowingUser(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public FollowingUser(Expression<Func<FollowingUser, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<FollowingUser> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<FollowingUser> _repo;
            
            if(db.TestMode){
                FollowingUser.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<FollowingUser>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<FollowingUser> GetRepo(){
            return GetRepo("","");
        }
        
        public static FollowingUser SingleOrDefault(Expression<Func<FollowingUser, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            FollowingUser single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static FollowingUser SingleOrDefault(Expression<Func<FollowingUser, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            FollowingUser single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<FollowingUser, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<FollowingUser, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<FollowingUser> Find(Expression<Func<FollowingUser, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<FollowingUser> Find(Expression<Func<FollowingUser, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<FollowingUser> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<FollowingUser> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<FollowingUser> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<FollowingUser> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<FollowingUser> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<FollowingUser> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.UserID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(FollowingUser)){
                FollowingUser compare=(FollowingUser)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.UserID.ToString();
                    }

        public string DescriptorColumn() {
            return "UserID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "UserID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<AppUser> AppUsers1
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _FollowingID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _FollowingID;
        public int FollowingID
        {
            get { return _FollowingID; }
            set
            {
                if(_FollowingID!=value){
                    _FollowingID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FollowingID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _Create_Date;
        public DateTime Create_Date
        {
            get { return _Create_Date; }
            set
            {
                if(_Create_Date!=value){
                    _Create_Date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Create_Date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<FollowingUser, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Comments table in the Pinjimu Database.
    /// </summary>
    public partial class Comment: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Comment> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Comment>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Comment> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Comment item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Comment item=new Comment();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Comment> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Comment(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Comment.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Comment>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Comment(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Comment(Expression<Func<Comment, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Comment> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Comment> _repo;
            
            if(db.TestMode){
                Comment.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Comment>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Comment> GetRepo(){
            return GetRepo("","");
        }
        
        public static Comment SingleOrDefault(Expression<Func<Comment, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Comment single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Comment SingleOrDefault(Expression<Func<Comment, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Comment single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Comment, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Comment, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Comment> Find(Expression<Func<Comment, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Comment> Find(Expression<Func<Comment, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Comment> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Comment> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Comment> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Comment> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Comment> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Comment> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
            			    return this.CommentX.ToString();
	                }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Comment)){
                Comment compare=(Comment)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
            			    return this.CommentX.ToString();
	                }

        public string DescriptorColumn() {
            return "Comment";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Comment";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardsImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _BoardsImagesMappingID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _CommentX;
        public string CommentX
        {
            get { return _CommentX; }
            set
            {
                if(_CommentX!=value){
                    _CommentX=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Comment");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _BoardsImagesMappingID;
        public int BoardsImagesMappingID
        {
            get { return _BoardsImagesMappingID; }
            set
            {
                if(_BoardsImagesMappingID!=value){
                    _BoardsImagesMappingID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="BoardsImagesMappingID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Comment, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Ratings table in the Pinjimu Database.
    /// </summary>
    public partial class Rating: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Rating> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Rating>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Rating> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Rating item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Rating item=new Rating();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Rating> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Rating(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Rating.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Rating>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Rating(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Rating(Expression<Func<Rating, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Rating> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Rating> _repo;
            
            if(db.TestMode){
                Rating.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Rating>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Rating> GetRepo(){
            return GetRepo("","");
        }
        
        public static Rating SingleOrDefault(Expression<Func<Rating, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Rating single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Rating SingleOrDefault(Expression<Func<Rating, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Rating single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Rating, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Rating, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Rating> Find(Expression<Func<Rating, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Rating> Find(Expression<Func<Rating, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Rating> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Rating> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Rating> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Rating> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Rating> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Rating> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ImageID";
        }

        public object KeyValue()
        {
            return this.ImageID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.RePins.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Rating)){
                Rating compare=(Rating)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ImageID;
        }
        
        public string DescriptorValue()
        {
                            return this.RePins.ToString();
                    }

        public string DescriptorColumn() {
            return "RePins";
        }
        public static string GetKeyColumn()
        {
            return "ImageID";
        }        
        public static string GetDescriptorColumn()
        {
            return "RePins";
        }
        
        #region ' Foreign Keys '
        public IQueryable<Image> Images
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Image.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _ImageID
                       select items;
            }
        }

        #endregion
        

        int _ImageID;
        public int ImageID
        {
            get { return _ImageID; }
            set
            {
                if(_ImageID!=value){
                    _ImageID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ImageID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _RePins;
        public int? RePins
        {
            get { return _RePins; }
            set
            {
                if(_RePins!=value){
                    _RePins=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RePins");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _Likes;
        public int? Likes
        {
            get { return _Likes; }
            set
            {
                if(_Likes!=value){
                    _Likes=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Likes");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _RatingX;
        public int? RatingX
        {
            get { return _RatingX; }
            set
            {
                if(_RatingX!=value){
                    _RatingX=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Rating");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Rating, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Boards table in the Pinjimu Database.
    /// </summary>
    public partial class Board: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Board> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Board>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Board> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Board item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Board item=new Board();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Board> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Board(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Board.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Board>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Board(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Board(Expression<Func<Board, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Board> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Board> _repo;
            
            if(db.TestMode){
                Board.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Board>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Board> GetRepo(){
            return GetRepo("","");
        }
        
        public static Board SingleOrDefault(Expression<Func<Board, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Board single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Board SingleOrDefault(Expression<Func<Board, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Board single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Board, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Board, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Board> Find(Expression<Func<Board, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Board> Find(Expression<Func<Board, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Board> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Board> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Board> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Board> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Board> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Board> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Board)){
                Board compare=(Board)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Name";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        public IQueryable<BoardContributor> BoardContributors
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardContributor.GetRepo();
                  return from items in repo.GetAll()
                       where items.BoardID == _ID
                       select items;
            }
        }

        public IQueryable<BoardsImagesMapping> BoardsImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.BoardsImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.BoardID == _ID
                       select items;
            }
        }

        public IQueryable<Category> Categories
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Category.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _CatID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name!=value){
                    _Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _CatID;
        public int CatID
        {
            get { return _CatID; }
            set
            {
                if(_CatID!=value){
                    _CatID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CatID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Board, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Points table in the Pinjimu Database.
    /// </summary>
    public partial class Point: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Point> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Point>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Point> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Point item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Point item=new Point();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Point> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Point(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Point.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Point>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Point(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Point(Expression<Func<Point, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Point> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Point> _repo;
            
            if(db.TestMode){
                Point.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Point>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Point> GetRepo(){
            return GetRepo("","");
        }
        
        public static Point SingleOrDefault(Expression<Func<Point, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Point single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Point SingleOrDefault(Expression<Func<Point, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Point single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Point, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Point, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Point> Find(Expression<Func<Point, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Point> Find(Expression<Func<Point, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Point> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Point> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Point> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Point> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Point> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Point> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Point)){
                Point compare=(Point)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Name";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<PointsHistory> PointsHistories
        {
            get
            {
                
                  var repo=SubSonic.POCOS.PointsHistory.GetRepo();
                  return from items in repo.GetAll()
                       where items.PointsID == _ID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name!=value){
                    _Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _Reward_Points;
        public short Reward_Points
        {
            get { return _Reward_Points; }
            set
            {
                if(_Reward_Points!=value){
                    _Reward_Points=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Reward_Points");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short _Max_Points;
        public short Max_Points
        {
            get { return _Max_Points; }
            set
            {
                if(_Max_Points!=value){
                    _Max_Points=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Max_Points");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Time_Limit;
        public string Time_Limit
        {
            get { return _Time_Limit; }
            set
            {
                if(_Time_Limit!=value){
                    _Time_Limit=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Time_Limit");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Point, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the PointsHistory table in the Pinjimu Database.
    /// </summary>
    public partial class PointsHistory: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<PointsHistory> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<PointsHistory>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<PointsHistory> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(PointsHistory item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                PointsHistory item=new PointsHistory();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<PointsHistory> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public PointsHistory(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                PointsHistory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<PointsHistory>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public PointsHistory(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public PointsHistory(Expression<Func<PointsHistory, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<PointsHistory> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<PointsHistory> _repo;
            
            if(db.TestMode){
                PointsHistory.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<PointsHistory>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<PointsHistory> GetRepo(){
            return GetRepo("","");
        }
        
        public static PointsHistory SingleOrDefault(Expression<Func<PointsHistory, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            PointsHistory single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static PointsHistory SingleOrDefault(Expression<Func<PointsHistory, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            PointsHistory single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<PointsHistory, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<PointsHistory, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<PointsHistory> Find(Expression<Func<PointsHistory, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<PointsHistory> Find(Expression<Func<PointsHistory, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<PointsHistory> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<PointsHistory> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<PointsHistory> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<PointsHistory> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<PointsHistory> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<PointsHistory> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.PointsID.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(PointsHistory)){
                PointsHistory compare=(PointsHistory)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.PointsID.ToString();
                    }

        public string DescriptorColumn() {
            return "PointsID";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "PointsID";
        }
        
        #region ' Foreign Keys '
        public IQueryable<Point> Points
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Point.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _PointsID
                       select items;
            }
        }

        public IQueryable<AppUser> AppUsers
        {
            get
            {
                
                  var repo=SubSonic.POCOS.AppUser.GetRepo();
                  return from items in repo.GetAll()
                       where items.ID == _UserID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _PointsID;
        public int PointsID
        {
            get { return _PointsID; }
            set
            {
                if(_PointsID!=value){
                    _PointsID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="PointsID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set
            {
                if(_UserID!=value){
                    _UserID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="UserID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        DateTime _Create_Date;
        public DateTime Create_Date
        {
            get { return _Create_Date; }
            set
            {
                if(_Create_Date!=value){
                    _Create_Date=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Create_Date");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<PointsHistory, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Articles table in the Pinjimu Database.
    /// </summary>
    public partial class Article: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Article> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Article>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Article> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Article item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Article item=new Article();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Article> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Article(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Article.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Article>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Article(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Article(Expression<Func<Article, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Article> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Article> _repo;
            
            if(db.TestMode){
                Article.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Article>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Article> GetRepo(){
            return GetRepo("","");
        }
        
        public static Article SingleOrDefault(Expression<Func<Article, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Article single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Article SingleOrDefault(Expression<Func<Article, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Article single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Article, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Article, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Article> Find(Expression<Func<Article, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Article> Find(Expression<Func<Article, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Article> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Article> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Article> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Article> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Article> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Article> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Title.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Article)){
                Article compare=(Article)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Title.ToString();
                    }

        public string DescriptorColumn() {
            return "Title";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Title";
        }
        
        #region ' Foreign Keys '
        #endregion
        

        string _Title;
        public string Title
        {
            get { return _Title; }
            set
            {
                if(_Title!=value){
                    _Title=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Title");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _RelImagePath;
        public string RelImagePath
        {
            get { return _RelImagePath; }
            set
            {
                if(_RelImagePath!=value){
                    _RelImagePath=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="RelImagePath");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Url;
        public string Url
        {
            get { return _Url; }
            set
            {
                if(_Url!=value){
                    _Url=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Url");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _Image_Height;
        public short? Image_Height
        {
            get { return _Image_Height; }
            set
            {
                if(_Image_Height!=value){
                    _Image_Height=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Height");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        short? _Image_Width;
        public short? Image_Width
        {
            get { return _Image_Width; }
            set
            {
                if(_Image_Width!=value){
                    _Image_Width=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Image_Width");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _FNV1a;
        public decimal? FNV1a
        {
            get { return _FNV1a; }
            set
            {
                if(_FNV1a!=value){
                    _FNV1a=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="FNV1a");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _MURMUR2;
        public decimal? MURMUR2
        {
            get { return _MURMUR2; }
            set
            {
                if(_MURMUR2!=value){
                    _MURMUR2=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="MURMUR2");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        decimal? _CRC64;
        public decimal? CRC64
        {
            get { return _CRC64; }
            set
            {
                if(_CRC64!=value){
                    _CRC64=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="CRC64");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _ArticleContent;
        public string ArticleContent
        {
            get { return _ArticleContent; }
            set
            {
                if(_ArticleContent!=value){
                    _ArticleContent=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ArticleContent");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                if(_Description!=value){
                    _Description=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Description");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Article, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
    
    
    /// <summary>
    /// A class which represents the Category table in the Pinjimu Database.
    /// </summary>
    public partial class Category: IActiveRecord
    {
    
        #region Built-in testing
        static TestRepository<Category> _testRepo;
        

        
        static void SetTestRepo(){
            _testRepo = _testRepo ?? new TestRepository<Category>(new SubSonic.POCOS.PinjimuDB());
        }
        public static void ResetTestRepo(){
            _testRepo = null;
            SetTestRepo();
        }
        public static void Setup(List<Category> testlist){
            SetTestRepo();
            foreach (var item in testlist)
            {
                _testRepo._items.Add(item);
            }
        }
        public static void Setup(Category item) {
            SetTestRepo();
            _testRepo._items.Add(item);
        }
        public static void Setup(int testItems) {
            SetTestRepo();
            for(int i=0;i<testItems;i++){
                Category item=new Category();
                _testRepo._items.Add(item);
            }
        }
        
        public bool TestMode = false;


        #endregion

        IRepository<Category> _repo;
        ITable tbl;
        bool _isNew;
        public bool IsNew(){
            return _isNew;
        }
        
        public void SetIsLoaded(bool isLoaded){
            _isLoaded=isLoaded;
            if(isLoaded)
                OnLoaded();
        }
        
        public void SetIsNew(bool isNew){
            _isNew=isNew;
        }
        bool _isLoaded;
        public bool IsLoaded(){
            return _isLoaded;
        }
                
        List<IColumn> _dirtyColumns;
        public bool IsDirty(){
            return _dirtyColumns.Count>0;
        }
        
        public List<IColumn> GetDirtyColumns (){
            return _dirtyColumns;
        }

        SubSonic.POCOS.PinjimuDB _db;
        public Category(string connectionString, string providerName) {

            _db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            Init();            
         }
        void Init(){
            TestMode=this._db.DataProvider.ConnectionString.Equals("test", StringComparison.InvariantCultureIgnoreCase);
            _dirtyColumns=new List<IColumn>();
            if(TestMode){
                Category.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Category>(_db);
            }
            tbl=_repo.GetTable();
            SetIsNew(true);
            OnCreated();       

        }
        
        public Category(){
             _db=new SubSonic.POCOS.PinjimuDB();
            Init();            
        }
        
       
        partial void OnCreated();
            
        partial void OnLoaded();
        
        partial void OnSaved();
        
        partial void OnChanged();
        
        public IList<IColumn> Columns{
            get{
                return tbl.Columns;
            }
        }

        public Category(Expression<Func<Category, bool>> expression):this() {

            SetIsLoaded(_repo.Load(this,expression));
        }
        
       
        
        internal static IRepository<Category> GetRepo(string connectionString, string providerName){
            SubSonic.POCOS.PinjimuDB db;
            if(String.IsNullOrEmpty(connectionString)){
                db=new SubSonic.POCOS.PinjimuDB();
            }else{
                db=new SubSonic.POCOS.PinjimuDB(connectionString, providerName);
            }
            IRepository<Category> _repo;
            
            if(db.TestMode){
                Category.SetTestRepo();
                _repo=_testRepo;
            }else{
                _repo = new SubSonicRepository<Category>(db);
            }
            return _repo;        
        }       
        
        internal static IRepository<Category> GetRepo(){
            return GetRepo("","");
        }
        
        public static Category SingleOrDefault(Expression<Func<Category, bool>> expression) {

            var repo = GetRepo();
            var results=repo.Find(expression);
            Category single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
                single.OnLoaded();
                single.SetIsLoaded(true);
                single.SetIsNew(false);
            }

            return single;
        }      
        
        public static Category SingleOrDefault(Expression<Func<Category, bool>> expression,string connectionString, string providerName) {
            var repo = GetRepo(connectionString,providerName);
            var results=repo.Find(expression);
            Category single=null;
            if(results.Count() > 0){
                single=results.ToList()[0];
            }

            return single;


        }
        
        
        public static bool Exists(Expression<Func<Category, bool>> expression,string connectionString, string providerName) {
           
            return All(connectionString,providerName).Any(expression);
        }        
        public static bool Exists(Expression<Func<Category, bool>> expression) {
           
            return All().Any(expression);
        }        

        public static IList<Category> Find(Expression<Func<Category, bool>> expression) {
            
            var repo = GetRepo();
            return repo.Find(expression).ToList();
        }
        
        public static IList<Category> Find(Expression<Func<Category, bool>> expression,string connectionString, string providerName) {

            var repo = GetRepo(connectionString,providerName);
            return repo.Find(expression).ToList();

        }
        public static IQueryable<Category> All(string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetAll();
        }
        public static IQueryable<Category> All() {
            return GetRepo().GetAll();
        }
        
        public static PagedList<Category> GetPaged(string sortBy, int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(sortBy, pageIndex, pageSize);
        }
      
        public static PagedList<Category> GetPaged(string sortBy, int pageIndex, int pageSize) {
            return GetRepo().GetPaged(sortBy, pageIndex, pageSize);
        }

        public static PagedList<Category> GetPaged(int pageIndex, int pageSize,string connectionString, string providerName) {
            return GetRepo(connectionString,providerName).GetPaged(pageIndex, pageSize);
            
        }


        public static PagedList<Category> GetPaged(int pageIndex, int pageSize) {
            return GetRepo().GetPaged(pageIndex, pageSize);
            
        }

        public string KeyName()
        {
            return "ID";
        }

        public object KeyValue()
        {
            return this.ID;
        }
        
        public void SetKeyValue(object value) {
            if (value != null && value!=DBNull.Value) {
                var settable = value.ChangeTypeTo<int>();
                this.GetType().GetProperty(this.KeyName()).SetValue(this, settable, null);
            }
        }
        
        public override string ToString(){
                            return this.Name.ToString();
                    }

        public override bool Equals(object obj){
            if(obj.GetType()==typeof(Category)){
                Category compare=(Category)obj;
                return compare.KeyValue()==this.KeyValue();
            }else{
                return base.Equals(obj);
            }
        }

        
        public override int GetHashCode() {
            return this.ID;
        }
        
        public string DescriptorValue()
        {
                            return this.Name.ToString();
                    }

        public string DescriptorColumn() {
            return "Name";
        }
        public static string GetKeyColumn()
        {
            return "ID";
        }        
        public static string GetDescriptorColumn()
        {
            return "Name";
        }
        
        #region ' Foreign Keys '
        public IQueryable<Board> Boards
        {
            get
            {
                
                  var repo=SubSonic.POCOS.Board.GetRepo();
                  return from items in repo.GetAll()
                       where items.CatID == _ID
                       select items;
            }
        }

        public IQueryable<CategoryImagesMapping> CategoryImagesMappings
        {
            get
            {
                
                  var repo=SubSonic.POCOS.CategoryImagesMapping.GetRepo();
                  return from items in repo.GetAll()
                       where items.CategoryID == _ID
                       select items;
            }
        }

        #endregion
        

        int _ID;
        public int ID
        {
            get { return _ID; }
            set
            {
                if(_ID!=value){
                    _ID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                if(_Name!=value){
                    _Name=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="Name");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }

        int? _ParentID;
        public int? ParentID
        {
            get { return _ParentID; }
            set
            {
                if(_ParentID!=value){
                    _ParentID=value;
                    var col=tbl.Columns.SingleOrDefault(x=>x.Name=="ParentID");
                    if(col!=null){
                        if(!_dirtyColumns.Any(x=>x.Name==col.Name) && _isLoaded){
                            _dirtyColumns.Add(col);
                        }
                    }
                    OnChanged();
                }
            }
        }



        public DbCommand GetUpdateCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToUpdateQuery(_db.Provider).GetCommand().ToDbCommand();
            
        }
        public DbCommand GetInsertCommand() {
 
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToInsertQuery(_db.Provider).GetCommand().ToDbCommand();
        }
        
        public DbCommand GetDeleteCommand() {
            if(TestMode)
                return _db.DataProvider.CreateCommand();
            else
                return this.ToDeleteQuery(_db.Provider).GetCommand().ToDbCommand();
        }
       
        
        public void Update(){
            Update(_db.DataProvider);
        }
        
        public void Update(IDataProvider provider){
        
            
            if(this._dirtyColumns.Count>0){
                _repo.Update(this,provider);
                _dirtyColumns.Clear();    
            }
            OnSaved();
       }
 
        public void Add(){
            Add(_db.DataProvider);
        }
        
        
       
        public void Add(IDataProvider provider){

            
            var key=KeyValue();
            if(key==null){
                var newKey=_repo.Add(this,provider);
                this.SetKeyValue(newKey);
            }else{
                _repo.Add(this,provider);
            }
            SetIsNew(false);
            OnSaved();
        }
        
                
        
        public void Save() {
            Save(_db.DataProvider);
        }      
        public void Save(IDataProvider provider) {
            
           
            if (_isNew) {
                Add(provider);
                
            } else {
                Update(provider);
            }
            
        }

        

        public void Delete(IDataProvider provider) {
                   
                 
            _repo.Delete(KeyValue());
            
                    }


        public void Delete() {
            Delete(_db.DataProvider);
        }


        public static void Delete(Expression<Func<Category, bool>> expression) {
            var repo = GetRepo();
            
       
            
            repo.DeleteMany(expression);
            
        }

        

        public void Load(IDataReader rdr) {
            Load(rdr, true);
        }
        public void Load(IDataReader rdr, bool closeReader) {
            if (rdr.Read()) {

                try {
                    rdr.Load(this);
                    SetIsNew(false);
                    SetIsLoaded(true);
                } catch {
                    SetIsLoaded(false);
                    throw;
                }
            }else{
                SetIsLoaded(false);
            }

            if (closeReader)
                rdr.Dispose();
        }
        

    } 
}
