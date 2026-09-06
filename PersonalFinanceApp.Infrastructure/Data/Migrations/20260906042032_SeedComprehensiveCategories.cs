using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalFinanceApp.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedComprehensiveCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000001"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000002"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000003"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000004"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("10000000-0000-0000-0000-000000000005"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                column: "ConcurrencyStamp",
                value: "290fd1bd-21b6-424d-a790-ca4710afba83");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "ColorHex", "CreatedAtUtc", "Icon", "Name", "ParentCategoryId", "UpdatedAtUtc", "UserId" },
                values: new object[,]
                {
                    { new Guid("244fb274-3a5e-40e2-b80b-5e0fee60bd1b"), "#F472B6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Gifts & Donations", null, null, null },
                    { new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Everyday", null, null, null },
                    { new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"), "#EF4444", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Debt", null, null, null },
                    { new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Transportation", null, null, null },
                    { new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Entertainment", null, null, null },
                    { new Guid("747990dd-6039-441c-8093-de2ee9bb010b"), "#64748B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Technology", null, null, null },
                    { new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"), "#14B8A6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Health", null, null, null },
                    { new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Travel", null, null, null },
                    { new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Housing", null, null, null },
                    { new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"), "#84CC16", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Pets", null, null, null },
                    { new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Insurance", null, null, null },
                    { new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Education", null, null, null },
                    { new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Children", null, null, null },
                    { new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Utilities", null, null, null },
                    { new Guid("dceee3e9-3670-4cbc-a617-6ac80cc214d2"), "#9CA3AF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", null, null, null },
                    { new Guid("08853dbe-992c-4f67-b731-cd0f097c1a25"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Laundry & Dry Cleaning", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("08960f83-02ad-4ab3-afe6-2749c787a98a"), "#64748B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Online Services", new Guid("747990dd-6039-441c-8093-de2ee9bb010b"), null, null },
                    { new Guid("0b40bf61-cd4c-4d4f-9930-8cff43d9bfe9"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Clothing", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("15bf2360-0d29-4036-8e36-ea8c7b61c31b"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Books", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("160d474a-83e0-4eea-a342-19513ec05bf8"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Gas", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("192ff9f6-d05b-48ec-b75c-e31dee1bdbd3"), "#84CC16", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Supplies", new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"), null, null },
                    { new Guid("1cf1d3d7-5057-4d3c-82e3-a9120ac15a1e"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Vehicle Payments", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("1d2a6df6-81a4-4947-bd49-7dbdfbff8af6"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cinema & Theater", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("1ec85c04-8c2b-4bbb-a316-d9eee99f9ace"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Registration & Licensing", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("1f6a08c2-8a10-48e1-9171-5859dc63c194"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hobbies", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("1fc92633-805c-4848-9016-2a21cc04a564"), "#14B8A6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Emergency", new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"), null, null },
                    { new Guid("21a0011a-7e1f-4be1-a060-3dd9a124716e"), "#EF4444", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"), null, null },
                    { new Guid("22d7e4b7-725d-444d-b44b-d3f2a239fcc0"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Life", new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"), null, null },
                    { new Guid("250341a6-076a-4cd5-bcb0-49e836633009"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("28edff1b-00a3-4f92-a85f-b40c12416ef4"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), null, null },
                    { new Guid("290ad8f2-7e95-4823-89e2-3c03465f5a98"), "#84CC16", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Food", new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"), null, null },
                    { new Guid("2b199eb2-6f9d-4b8c-b18a-0176b03bfb20"), "#EF4444", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Credit Cards", new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"), null, null },
                    { new Guid("2bbb8078-cacf-4a85-b828-08946105747b"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dining", new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), null, null },
                    { new Guid("34faf741-74c3-42cc-aef7-3ed6bfa22488"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Photography", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("388ef714-c2ac-45d6-aea0-b681a5d7e214"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Repairs", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("38e811b5-67de-4b01-b360-cc6042a41eef"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Outdoor Activities", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("3a2cb2c2-7783-42e3-b468-8fe84ae60fb7"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hotels", new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), null, null },
                    { new Guid("3a603453-0ca4-4fe5-8bd7-62dee510b486"), "#EF4444", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other Loans", new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"), null, null },
                    { new Guid("3ab636cb-9efc-4f07-9bf5-0f05bc7373bd"), "#84CC16", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Vet & Medications", new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"), null, null },
                    { new Guid("3dfe2adc-3067-4cbf-b8b0-4a62e492c563"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Groceries", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("403fc629-37d2-4afe-9518-57e6f15455b6"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Supplies", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("44988d2e-a13d-4a23-b8c8-034824bd8f04"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hair & Beauty", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("470449ec-2603-44d5-bfda-e8475c369228"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Car Wash", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("476ee32a-748c-4e3c-ad5a-05554729e57e"), "#64748B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Domains & Hosting", new Guid("747990dd-6039-441c-8093-de2ee9bb010b"), null, null },
                    { new Guid("581b99b6-9564-49f4-811c-572231945d87"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Allowance", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("587ab4de-997d-4abc-b7d9-c038a58032df"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Monthly Fee", new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), null, null },
                    { new Guid("5b9b7f79-f4b8-4ffb-9015-40cb6485c6f5"), "#14B8A6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Pharmacy", new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"), null, null },
                    { new Guid("5ba8fa64-353e-4deb-a9eb-38fcfb6c8c10"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Internet", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("60c376c0-9f71-4a47-95cf-3b30102742d5"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Health", new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"), null, null },
                    { new Guid("62935e1c-fbc0-4033-b31c-788567b784f1"), "#64748B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("747990dd-6039-441c-8093-de2ee9bb010b"), null, null },
                    { new Guid("656b821a-4a5e-4ab3-ad98-664cd726a827"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Water", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("669b13a0-cf32-49f9-8543-d316ef4ba807"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Vehicle", new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"), null, null },
                    { new Guid("66fce58f-8ee7-424d-bbe3-2eafd35840b9"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Maintenance", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("679dd9ed-b574-4df2-8911-d258507738e7"), "#14B8A6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Specialists", new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"), null, null },
                    { new Guid("6bc2d146-4522-46bf-bb8c-58fe50cab6ee"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Furniture", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("6e51f026-eb45-4d49-898c-da57c4d197f2"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Education", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("70817266-9630-4d44-b5ad-77525b10e25d"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Electricity", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("73a5cad0-5c6f-4409-ab64-bcd616fe8284"), "#84CC16", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Toys", new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"), null, null },
                    { new Guid("74b7831c-240c-4eaf-8f7e-d8d890ca290d"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), null, null },
                    { new Guid("76082b7d-9be5-4996-ba9b-d6bdb19ca85c"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Childcare", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("785464c5-3da7-4c0e-9d92-878ab3075a85"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Toys", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("7cd15891-9c87-4268-a99a-d52368c6a60f"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Cable", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("7d1867e8-5883-4b1a-a80a-be17a65e00cb"), "#9CA3AF", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other Expenses", new Guid("dceee3e9-3670-4cbc-a617-6ac80cc214d2"), null, null },
                    { new Guid("7dc9e1a2-ce15-4aa5-aee3-8b042f7d802d"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Public Transit", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("7ea12b1b-73eb-4374-9d7c-b2709cc4b051"), "#EF4444", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Mortgage", new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"), null, null },
                    { new Guid("834da54d-e14b-477f-99d0-9afb66468121"), "#64748B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Hardware", new Guid("747990dd-6039-441c-8093-de2ee9bb010b"), null, null },
                    { new Guid("85b35237-c2d8-4a18-8f99-928d72591808"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Entertainment", new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), null, null },
                    { new Guid("984790f0-f7a6-4862-93c8-b7d44799a4be"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Courses", new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), null, null },
                    { new Guid("9b27237a-ce27-400b-a2b2-9436fd3a9df5"), "#F472B6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("244fb274-3a5e-40e2-b80b-5e0fee60bd1b"), null, null },
                    { new Guid("9cd0d6ac-64b0-40a0-b5e7-6972493377f1"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Airfare", new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), null, null },
                    { new Guid("9dafcf6a-9892-4048-ada6-370706b4dc64"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Home", new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"), null, null },
                    { new Guid("a12fa006-213e-491b-8c13-c0094fdfa280"), "#F472B6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Charity", new Guid("244fb274-3a5e-40e2-b80b-5e0fee60bd1b"), null, null },
                    { new Guid("a65d827e-87e8-4075-bffe-c65ab578e128"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Home Goods", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("aa9eb638-848f-44f3-8b1d-0d983e8bd9b6"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Personal Care", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("ab7286e5-ec96-40f8-884c-d30d149c84b1"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Medical Expenses", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("aed1a3f2-1211-4100-ba25-da55af9ddef8"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Books", new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), null, null },
                    { new Guid("af0e21a5-1927-4aea-ac41-dfeda4a4d0cc"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Fuel", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("b431cd2c-98fe-4eb1-b3d9-35df37dfc19f"), "#84CC16", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"), null, null },
                    { new Guid("b6daaa27-cf8d-4166-a109-2b616cf9841b"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Games", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("bae067af-7ae9-4d0f-9b51-87b67e84b968"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Home Improvements", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("bb945eac-a7cb-4a07-84a8-7f782680f922"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Movies", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("bbcedb10-ee32-4443-8cfd-849c7771f29a"), "#14B8A6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Doctors", new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"), null, null },
                    { new Guid("bf09a7b1-4c6d-4c14-b468-bb8ebfbcdfed"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("c32895ab-21a7-491f-a7f7-ba895bd0eba2"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Property Taxes", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("c420e2e2-89f2-4489-8a24-03443db1a0cf"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Television", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("c518203d-2922-479e-a14b-d50f029dcbbf"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("ca73e76e-efad-40f0-a86e-f17801d9e2ac"), "#F472B6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Gifts", new Guid("244fb274-3a5e-40e2-b80b-5e0fee60bd1b"), null, null },
                    { new Guid("d2c2f5de-facd-41d8-815a-a311c061d642"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("d44a7913-b964-4068-bd78-0e581f906d59"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Concerts & Shows", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("d62b463c-92ae-43b0-a055-37fd29c9193f"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Lawn & Garden", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("db0fd62e-7b25-49fc-92bd-f7781a1e6f4b"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Trash", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("dbc9d96f-9858-4b75-8784-6e68da415e28"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Activities", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("dbcdc4fa-4395-4786-a335-1c43878c44c4"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Rent or Mortgage", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("dbf68cf2-df95-45dd-9332-0ce6c4efb750"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Dining Out", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null },
                    { new Guid("e0194d83-e9e6-4b13-9ba7-39fa8a98d36e"), "#EF4444", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Student Loans", new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"), null, null },
                    { new Guid("e033e1e1-44e7-41fb-be08-7a287f4eb83a"), "#64748B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Software", new Guid("747990dd-6039-441c-8093-de2ee9bb010b"), null, null },
                    { new Guid("e500b73d-8f24-45e3-bbb5-3010f96efac5"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Clothing", new Guid("cca984db-acc2-4025-9fbf-42403506eab4"), null, null },
                    { new Guid("e628eade-9382-4917-af77-fd2824d85d1e"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("ebcdfa02-0206-45a1-b39d-d12814d1fc75"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Sports", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("ec37be8c-c5f2-44bc-aedf-ebba84ee428e"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Certifications", new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), null, null },
                    { new Guid("f0516a5c-5d9b-45b0-b7d8-ee05e90742d7"), "#EAB308", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Phone", new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"), null, null },
                    { new Guid("f3df04bc-4662-4293-aedc-5e1ed630a866"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"), null, null },
                    { new Guid("fa985bb1-50a9-4f48-8540-1c1392cf9a87"), "#6366F1", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Moving", new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"), null, null },
                    { new Guid("fbdfd64b-5035-41a8-b30a-226efbeabfac"), "#06B6D4", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Transportation", new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"), null, null },
                    { new Guid("fc229278-d418-4947-b4a3-7535c9dbeaa2"), "#14B8A6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"), null, null },
                    { new Guid("fcba3eff-bba2-4f8d-987c-1851d6d73865"), "#EC4899", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Music", new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"), null, null },
                    { new Guid("ff1bfbd7-4c49-4ecc-aec2-52d6df9788d1"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Tuition", new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"), null, null },
                    { new Guid("ff1f078f-3a61-4be6-b3dd-0699709412e3"), "#F97316", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Other", new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"), null, null },
                    { new Guid("ffcc2f24-f25f-47e8-9557-31c6257fd5b9"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Subscriptions", new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"), null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("08853dbe-992c-4f67-b731-cd0f097c1a25"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("08960f83-02ad-4ab3-afe6-2749c787a98a"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("0b40bf61-cd4c-4d4f-9930-8cff43d9bfe9"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("15bf2360-0d29-4036-8e36-ea8c7b61c31b"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("160d474a-83e0-4eea-a342-19513ec05bf8"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("192ff9f6-d05b-48ec-b75c-e31dee1bdbd3"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("1cf1d3d7-5057-4d3c-82e3-a9120ac15a1e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("1d2a6df6-81a4-4947-bd49-7dbdfbff8af6"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("1ec85c04-8c2b-4bbb-a316-d9eee99f9ace"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("1f6a08c2-8a10-48e1-9171-5859dc63c194"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("1fc92633-805c-4848-9016-2a21cc04a564"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("21a0011a-7e1f-4be1-a060-3dd9a124716e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("22d7e4b7-725d-444d-b44b-d3f2a239fcc0"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("250341a6-076a-4cd5-bcb0-49e836633009"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("28edff1b-00a3-4f92-a85f-b40c12416ef4"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("290ad8f2-7e95-4823-89e2-3c03465f5a98"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("2b199eb2-6f9d-4b8c-b18a-0176b03bfb20"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("2bbb8078-cacf-4a85-b828-08946105747b"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("34faf741-74c3-42cc-aef7-3ed6bfa22488"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("388ef714-c2ac-45d6-aea0-b681a5d7e214"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("38e811b5-67de-4b01-b360-cc6042a41eef"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3a2cb2c2-7783-42e3-b468-8fe84ae60fb7"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3a603453-0ca4-4fe5-8bd7-62dee510b486"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3ab636cb-9efc-4f07-9bf5-0f05bc7373bd"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("3dfe2adc-3067-4cbf-b8b0-4a62e492c563"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("403fc629-37d2-4afe-9518-57e6f15455b6"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("44988d2e-a13d-4a23-b8c8-034824bd8f04"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("470449ec-2603-44d5-bfda-e8475c369228"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("476ee32a-748c-4e3c-ad5a-05554729e57e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("581b99b6-9564-49f4-811c-572231945d87"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("587ab4de-997d-4abc-b7d9-c038a58032df"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("5b9b7f79-f4b8-4ffb-9015-40cb6485c6f5"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("5ba8fa64-353e-4deb-a9eb-38fcfb6c8c10"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("60c376c0-9f71-4a47-95cf-3b30102742d5"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("62935e1c-fbc0-4033-b31c-788567b784f1"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("656b821a-4a5e-4ab3-ad98-664cd726a827"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("669b13a0-cf32-49f9-8543-d316ef4ba807"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("66fce58f-8ee7-424d-bbe3-2eafd35840b9"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("679dd9ed-b574-4df2-8911-d258507738e7"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("6bc2d146-4522-46bf-bb8c-58fe50cab6ee"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("6e51f026-eb45-4d49-898c-da57c4d197f2"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("70817266-9630-4d44-b5ad-77525b10e25d"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("73a5cad0-5c6f-4409-ab64-bcd616fe8284"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("74b7831c-240c-4eaf-8f7e-d8d890ca290d"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("76082b7d-9be5-4996-ba9b-d6bdb19ca85c"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("785464c5-3da7-4c0e-9d92-878ab3075a85"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("7cd15891-9c87-4268-a99a-d52368c6a60f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("7d1867e8-5883-4b1a-a80a-be17a65e00cb"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("7dc9e1a2-ce15-4aa5-aee3-8b042f7d802d"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("7ea12b1b-73eb-4374-9d7c-b2709cc4b051"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("834da54d-e14b-477f-99d0-9afb66468121"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("85b35237-c2d8-4a18-8f99-928d72591808"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("984790f0-f7a6-4862-93c8-b7d44799a4be"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("9b27237a-ce27-400b-a2b2-9436fd3a9df5"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("9cd0d6ac-64b0-40a0-b5e7-6972493377f1"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("9dafcf6a-9892-4048-ada6-370706b4dc64"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("a12fa006-213e-491b-8c13-c0094fdfa280"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("a65d827e-87e8-4075-bffe-c65ab578e128"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("aa9eb638-848f-44f3-8b1d-0d983e8bd9b6"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ab7286e5-ec96-40f8-884c-d30d149c84b1"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("aed1a3f2-1211-4100-ba25-da55af9ddef8"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("af0e21a5-1927-4aea-ac41-dfeda4a4d0cc"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("b431cd2c-98fe-4eb1-b3d9-35df37dfc19f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("b6daaa27-cf8d-4166-a109-2b616cf9841b"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("bae067af-7ae9-4d0f-9b51-87b67e84b968"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("bb945eac-a7cb-4a07-84a8-7f782680f922"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("bbcedb10-ee32-4443-8cfd-849c7771f29a"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("bf09a7b1-4c6d-4c14-b468-bb8ebfbcdfed"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("c32895ab-21a7-491f-a7f7-ba895bd0eba2"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("c420e2e2-89f2-4489-8a24-03443db1a0cf"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("c518203d-2922-479e-a14b-d50f029dcbbf"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ca73e76e-efad-40f0-a86e-f17801d9e2ac"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d2c2f5de-facd-41d8-815a-a311c061d642"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d44a7913-b964-4068-bd78-0e581f906d59"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("d62b463c-92ae-43b0-a055-37fd29c9193f"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("db0fd62e-7b25-49fc-92bd-f7781a1e6f4b"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("dbc9d96f-9858-4b75-8784-6e68da415e28"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("dbcdc4fa-4395-4786-a335-1c43878c44c4"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("dbf68cf2-df95-45dd-9332-0ce6c4efb750"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("e0194d83-e9e6-4b13-9ba7-39fa8a98d36e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("e033e1e1-44e7-41fb-be08-7a287f4eb83a"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("e500b73d-8f24-45e3-bbb5-3010f96efac5"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("e628eade-9382-4917-af77-fd2824d85d1e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ebcdfa02-0206-45a1-b39d-d12814d1fc75"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ec37be8c-c5f2-44bc-aedf-ebba84ee428e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("f0516a5c-5d9b-45b0-b7d8-ee05e90742d7"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("f3df04bc-4662-4293-aedc-5e1ed630a866"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("fa985bb1-50a9-4f48-8540-1c1392cf9a87"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("fbdfd64b-5035-41a8-b30a-226efbeabfac"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("fc229278-d418-4947-b4a3-7535c9dbeaa2"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("fcba3eff-bba2-4f8d-987c-1851d6d73865"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ff1bfbd7-4c49-4ecc-aec2-52d6df9788d1"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ff1f078f-3a61-4be6-b3dd-0699709412e3"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("ffcc2f24-f25f-47e8-9557-31c6257fd5b9"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("244fb274-3a5e-40e2-b80b-5e0fee60bd1b"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("2b22a05b-fef1-4d3f-9178-99ebe12519b3"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("43c72a81-8ff7-4bac-a0fa-2d2ad50bfa6a"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("47129cd9-9a13-4ea7-914d-d09b59bb21f6"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("6271d373-31e6-4b8d-b431-9ae25de97daf"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("747990dd-6039-441c-8093-de2ee9bb010b"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("80ce73e4-5200-4ecd-9011-ea1c5f43be6e"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("8c6e5427-76ee-4a4b-9c37-f223ec668754"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("92405c01-5cc5-458c-9fc2-7110d4b023ee"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("a90e1c7a-c110-4a33-86fe-6cf5452af73d"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("b6ea5813-ebdc-4723-9cde-50c2165dd094"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("bb056a43-2fb9-4b1b-8d49-04cf324a1ede"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("cca984db-acc2-4025-9fbf-42403506eab4"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("da56e738-ee2b-45c0-b40c-2d9112b63adf"));

            migrationBuilder.DeleteData(
                table: "categories",
                keyColumn: "Id",
                keyValue: new Guid("dceee3e9-3670-4cbc-a617-6ac80cc214d2"));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-0000-0000-0000-000000000001",
                column: "ConcurrencyStamp",
                value: "6bd3f737-d8ed-448d-ac33-54e3a2db486e");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "ColorHex", "CreatedAtUtc", "Icon", "Name", "ParentCategoryId", "UpdatedAtUtc", "UserId" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "#3B82F6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Housing", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "#10B981", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Food & Dining", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "#F59E0B", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Transportation", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "#8B5CF6", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Utilities", null, null, null },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "#059669", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Income", null, null, null }
                });
        }
    }
}
