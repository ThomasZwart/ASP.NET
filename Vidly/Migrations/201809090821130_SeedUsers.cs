namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'302fe61b-5510-40cc-b8a5-d95e2f3d8e64', N'admin@vidly.com', 0, N'AA9/NvFSadl66Vm+/+MI041XM4wqVql9n37xnC4WG7e+ztDXnUA0fCDhV8wMS/CQQQ==', N'6f22ff26-d3bc-4530-acc7-0034f2130f1c', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'335fa46d-c58a-4883-aa23-73a824a6fae8', N'thomas.black@live.nl', 0, N'AOxJVuCvjvT6I1D3X8gSAU3bdosVE6dI3r6JHZwB0hh9GvgfYV0H6xX0hcXj+g79CQ==', N'941089e7-78bf-49ef-b1d9-06c1791a5f8c', NULL, 0, 0, NULL, 1, 0, N'thomas.black@live.nl')
                INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'94b0fc11-cbaa-4a2e-b0f7-5c0bfc89bf9e', N'guest@vidly.com', 0, N'AI3EAtg9iphCkQyswuuxmSJ1TNDYnnfRRL+CLQbHIaqbCXI3PAWlX1NT9Hs92NZyiQ==', N'fed96176-96df-4683-9155-f48cbe926835', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
                INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'58243928-92a8-4b78-9de3-567439614317', N'CanManageMovie')
                INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'302fe61b-5510-40cc-b8a5-d95e2f3d8e64', N'58243928-92a8-4b78-9de3-567439614317')");
        }
        
        public override void Down()
        {
        }
    }
}
