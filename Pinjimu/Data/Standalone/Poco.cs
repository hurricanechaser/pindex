


using System;
namespace Pinjimu.Data.Standalone {

public class User {

public int ID { get;set; }
public string Name { get;set; }
public string Password { get;set; }
}public class Stores {

public int ID { get;set; }
public string Url { get;set; }
public short? Image_Height { get;set; }
public short? Image_Width { get;set; }
public long? FNV1a { get;set; }
public long? MURMUR2 { get;set; }
public long? CRC64 { get;set; }
public string Title { get;set; }
public string RelImagePath { get;set; }
}public class Images {

public int ID { get;set; }
public short? Image_Height { get;set; }
public short? Image_Width { get;set; }
public string RelativeImage_Path { get;set; }
public DateTime? Date { get;set; }
public bool? Tagged { get;set; }
public long? CRC64 { get;set; }
public long? FNV1a { get;set; }
public long? MURMUR2 { get;set; }
public bool? Uploaded { get;set; }
public bool? Verified { get;set; }
}public class Facebook {

public long id { get;set; }
public string last_name { get;set; }
public string link { get;set; }
public string locale { get;set; }
public string name { get;set; }
public string timezone { get;set; }
public DateTime? updated_time { get;set; }
public string first_name { get;set; }
public string gender { get;set; }
}public class vw_ImagesSNo {

public int ID { get;set; }
public string RelativeImage_Path { get;set; }
public bool? Tagged { get;set; }
public string SNo { get;set; }
}public class UserBatchAssigned {

public int ID { get;set; }
public int UserID { get;set; }
public int BatchStart { get;set; }
public int BatchEnd { get;set; }
}public class CategoryImagesMapping {

public int CategoryID { get;set; }
public int ImageID { get;set; }
public int ID { get;set; }
public int? UserID { get;set; }
}public class AppUsers {

public int ID { get;set; }
public DateTime Create_date { get;set; }
public string Speciality { get;set; }
public string Name { get;set; }
public string Password { get;set; }
public string Email { get;set; }
public string Avatar { get;set; }
public string FirstName { get;set; }
public string About { get;set; }
public string Location { get;set; }
public string Website { get;set; }
public string Invite { get;set; }
public long? facebookid { get;set; }
public int? Points { get;set; }
}public class vw_ImgCategory {

public int ID { get;set; }
public int CategoryID { get;set; }
public string Name { get;set; }
}public class vw_Cat {

public int ID { get;set; }
public int CategoryID { get;set; }
public string Name { get;set; }
}public class Review {

public int ID { get;set; }
public int BIMID { get;set; }
public string Question { get;set; }
public string Answer { get;set; }
public int? UserID { get;set; }
public string SessionID { get;set; }
}public class BoardContributor {

public int BoardID { get;set; }
public int ID { get;set; }
public int ContributorID { get;set; }
}public class BoardsImagesMapping {

public int ImageID { get;set; }
public int ID { get;set; }
public string Image_Title { get;set; }
public int? UserID { get;set; }
public string Source { get;set; }
public int? Rating { get;set; }
public int? BoardID { get;set; }
}public class Likes {

public int ID { get;set; }
public int BoardsImagesMappingID { get;set; }
public int UserID { get;set; }
}public class vw_BoardCategory {

public int ID { get;set; }
public int? BoardID { get;set; }
public string Name { get;set; }
public string Source { get;set; }
}public class vw_Images4Tagging {

public int UserID { get;set; }
public int ID { get;set; }
public string RelativeImage_Path { get;set; }
public bool? Tagged { get;set; }
public long? CRC64 { get;set; }
public long? FNV1a { get;set; }
public long? MURMUR2 { get;set; }
public short? Image_Height { get;set; }
public short? Image_Width { get;set; }
public int? CategoryID { get;set; }
public string CategoryName { get;set; }
public string Image_Title { get;set; }
public int? Rating { get;set; }
}public class vw_UserComments {

public int UserID { get;set; }
public int BoardsImagesMappingID { get;set; }
public string FirstName { get;set; }
public string Comment { get;set; }
public string Name { get;set; }
public string Avatar { get;set; }
}public class Prize {

public int ID { get;set; }
public string Prize_Name { get;set; }
public string User_Alert { get;set; }
}public class Roulette {

public int ID { get;set; }
public int Start_Angle { get;set; }
public int End_Angle { get;set; }
public int PrizeID { get;set; }
}public class PrizeHistory {

public int ID { get;set; }
public int PrizeID { get;set; }
public int UserID { get;set; }
public DateTime Create_date { get;set; }
}public class vw_FollowingUser {

public int FollowingID { get;set; }
public int UserID { get;set; }
public int Following_Count { get;set; }
public int Follower_Count { get;set; }
public bool? Follow_Status { get;set; }
public string Pins_XML { get;set; }
public string UserName { get;set; }
public string Avatar { get;set; }
public string FullName { get;set; }
}public class FollowingUser {

public int ID { get;set; }
public int UserID { get;set; }
public int FollowingID { get;set; }
public DateTime Create_Date { get;set; }
}public class vw_FollowCount {

public int UserID { get;set; }
public int Following_Count { get;set; }
public int Follower_Count { get;set; }
}public class vw_FollowerUser {

public int FollowerID { get;set; }
public int UserID { get;set; }
public int Following_Count { get;set; }
public int Follower_Count { get;set; }
public string Pins_XML { get;set; }
public string FullName { get;set; }
public string UserName { get;set; }
public string Avatar { get;set; }
}public class Comments {

public int ID { get;set; }
public int BoardsImagesMappingID { get;set; }
public int UserID { get;set; }
public string Comment { get;set; }
}public class Ratings {

public int ImageID { get;set; }
public int? RePins { get;set; }
public int? Likes { get;set; }
public int? Rating { get;set; }
}public class vw_User {

public int Following_Count { get;set; }
public int Follower_Count { get;set; }
public int ID { get;set; }
public string Name { get;set; }
public string Email { get;set; }
public string Avatar { get;set; }
public string FirstName { get;set; }
public string About { get;set; }
public string Location { get;set; }
public string Website { get;set; }
public int? Points { get;set; }
}public class vw_Images {

public int ID { get;set; }
public int BIMID { get;set; }
public int? BoardID { get;set; }
public string Source { get;set; }
public int? Rating { get;set; }
public short? Image_Height { get;set; }
public short? Image_Width { get;set; }
public string RelativeImage_Path { get;set; }
public long? CRC64 { get;set; }
public long? FNV1a { get;set; }
public long? MURMUR2 { get;set; }
public bool? Uploaded { get;set; }
public long? PinID { get;set; }
public string Image_Title { get;set; }
public string BoardName { get;set; }
public int? UserID { get;set; }
public int? BoardUserID { get;set; }
}public class Boards {

public int ID { get;set; }
public int CatID { get;set; }
public int UserID { get;set; }
public string Name { get;set; }
}public class vw_ImagesforCategories {

public int ID { get;set; }
public int BIMID { get;set; }
public int? BoardID { get;set; }
public string Source { get;set; }
public int? Rating { get;set; }
public short? Image_Height { get;set; }
public short? Image_Width { get;set; }
public string RelativeImage_Path { get;set; }
public long? CRC64 { get;set; }
public long? FNV1a { get;set; }
public long? MURMUR2 { get;set; }
public bool? Uploaded { get;set; }
public long? PinID { get;set; }
public string Image_Title { get;set; }
public string BoardName { get;set; }
public int? UserID { get;set; }
public int? BoardUserID { get;set; }
}public class Points {

public int ID { get;set; }
public string Name { get;set; }
public short Reward_Points { get;set; }
public short Max_Points { get;set; }
public string Time_Limit { get;set; }
}public class PointsHistory {

public int ID { get;set; }
public int PointsID { get;set; }
public int UserID { get;set; }
public DateTime Create_Date { get;set; }
}public class Articles {

public int ID { get;set; }
public string Url { get;set; }
public short? Image_Height { get;set; }
public short? Image_Width { get;set; }
public long? FNV1a { get;set; }
public long? MURMUR2 { get;set; }
public long? CRC64 { get;set; }
public string ArticleContent { get;set; }
public string Description { get;set; }
public string Title { get;set; }
public string RelImagePath { get;set; }
}public class Category {

public int ID { get;set; }
public string Name { get;set; }
public int? ParentID { get;set; }
}}

