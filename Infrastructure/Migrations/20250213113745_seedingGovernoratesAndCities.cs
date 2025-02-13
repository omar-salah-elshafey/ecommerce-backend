using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedingGovernoratesAndCities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Governorates",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("01111111-1111-1111-1111-111111111111"), "الإسكندرية" },
                    { new Guid("02111111-1111-1111-1111-111111111111"), "البحيرة" },
                    { new Guid("03111111-1111-1111-1111-111111111111"), "كفر الشيخ" },
                    { new Guid("04111111-1111-1111-1111-111111111111"), "الغربية" },
                    { new Guid("05111111-1111-1111-1111-111111111111"), "المنوفية" },
                    { new Guid("06111111-1111-1111-1111-111111111111"), "الدقهلية" },
                    { new Guid("07111111-1111-1111-1111-111111111111"), "الشرقية" },
                    { new Guid("08111111-1111-1111-1111-111111111111"), "دمياط" },
                    { new Guid("09111111-1111-1111-1111-111111111111"), "القليوبية" },
                    { new Guid("10111111-1111-1111-1111-111111111111"), "القاهرة" }
                });

            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "GovernorateId", "Name" },
                values: new object[,]
                {
                    { new Guid("10111111-1111-1111-1111-101111111112"), new Guid("10111111-1111-1111-1111-111111111111"), "القاهرة" },
                    { new Guid("10111111-1111-1111-1111-101111111113"), new Guid("10111111-1111-1111-1111-111111111111"), "مدينة نصر" },
                    { new Guid("10111111-1111-1111-1111-101111111114"), new Guid("10111111-1111-1111-1111-111111111111"), "مصر الجديدة" },
                    { new Guid("10111111-1111-1111-1111-101111111115"), new Guid("10111111-1111-1111-1111-111111111111"), "حلوان" },
                    { new Guid("10111111-1111-1111-1111-101111111116"), new Guid("10111111-1111-1111-1111-111111111111"), "الجيزة" },
                    { new Guid("11111111-1111-1111-1111-111111111112"), new Guid("01111111-1111-1111-1111-111111111111"), "الإسكندرية" },
                    { new Guid("11111111-1111-1111-1111-111111111113"), new Guid("01111111-1111-1111-1111-111111111111"), "برج العرب" },
                    { new Guid("21111111-1111-1111-1111-211111111112"), new Guid("02111111-1111-1111-1111-111111111111"), "دمنهور" },
                    { new Guid("21111111-1111-1111-1111-211111111113"), new Guid("02111111-1111-1111-1111-111111111111"), "رشيد" },
                    { new Guid("21111111-1111-1111-1111-211111111114"), new Guid("02111111-1111-1111-1111-111111111111"), "كفر الدوار" },
                    { new Guid("21111111-1111-1111-1111-211111111115"), new Guid("02111111-1111-1111-1111-111111111111"), "إدكو" },
                    { new Guid("31111111-1111-1111-1111-311111111112"), new Guid("03111111-1111-1111-1111-111111111111"), "كفر الشيخ" },
                    { new Guid("31111111-1111-1111-1111-311111111113"), new Guid("03111111-1111-1111-1111-111111111111"), "دسوق" },
                    { new Guid("31111111-1111-1111-1111-311111111114"), new Guid("03111111-1111-1111-1111-111111111111"), "بيلا" },
                    { new Guid("31111111-1111-1111-1111-311111111115"), new Guid("03111111-1111-1111-1111-111111111111"), "فوه" },
                    { new Guid("41111111-1111-1111-1111-411111111112"), new Guid("04111111-1111-1111-1111-111111111111"), "طنطا" },
                    { new Guid("41111111-1111-1111-1111-411111111113"), new Guid("04111111-1111-1111-1111-111111111111"), "المحلة الكبرى" },
                    { new Guid("41111111-1111-1111-1111-411111111114"), new Guid("04111111-1111-1111-1111-111111111111"), "كفر الزيات" },
                    { new Guid("41111111-1111-1111-1111-411111111115"), new Guid("04111111-1111-1111-1111-111111111111"), "سمنود" },
                    { new Guid("41111111-1111-1111-1111-411111111116"), new Guid("04111111-1111-1111-1111-111111111111"), "زفتى" },
                    { new Guid("51111111-1111-1111-1111-511111111112"), new Guid("05111111-1111-1111-1111-111111111111"), "شبين الكوم" },
                    { new Guid("51111111-1111-1111-1111-511111111113"), new Guid("05111111-1111-1111-1111-111111111111"), "قويسنا" },
                    { new Guid("51111111-1111-1111-1111-511111111114"), new Guid("05111111-1111-1111-1111-111111111111"), "منوف" },
                    { new Guid("51111111-1111-1111-1111-511111111115"), new Guid("05111111-1111-1111-1111-111111111111"), "تلا" },
                    { new Guid("61111111-1111-1111-1111-611111111112"), new Guid("06111111-1111-1111-1111-111111111111"), "المنصورة" },
                    { new Guid("61111111-1111-1111-1111-611111111113"), new Guid("06111111-1111-1111-1111-111111111111"), "ميت غمر" },
                    { new Guid("61111111-1111-1111-1111-611111111114"), new Guid("06111111-1111-1111-1111-111111111111"), "أجا" },
                    { new Guid("61111111-1111-1111-1111-611111111115"), new Guid("06111111-1111-1111-1111-111111111111"), "دكرنس" },
                    { new Guid("61111111-1111-1111-1111-611111111116"), new Guid("06111111-1111-1111-1111-111111111111"), "المنزلة" },
                    { new Guid("61111111-1111-1111-1111-611111111117"), new Guid("06111111-1111-1111-1111-111111111111"), "الدراكسة" },
                    { new Guid("61111111-1111-1111-1111-611111111118"), new Guid("06111111-1111-1111-1111-111111111111"), "نبروه" },
                    { new Guid("71111111-1111-1111-1111-711111111112"), new Guid("07111111-1111-1111-1111-111111111111"), "الزقازيق" },
                    { new Guid("71111111-1111-1111-1111-711111111113"), new Guid("07111111-1111-1111-1111-111111111111"), "العاشر من رمضان" },
                    { new Guid("71111111-1111-1111-1111-711111111114"), new Guid("07111111-1111-1111-1111-111111111111"), "بلبيس" },
                    { new Guid("71111111-1111-1111-1111-711111111115"), new Guid("07111111-1111-1111-1111-111111111111"), "منيا القمح" },
                    { new Guid("71111111-1111-1111-1111-711111111116"), new Guid("07111111-1111-1111-1111-111111111111"), "أبو كبير" },
                    { new Guid("81111111-1111-1111-1111-811111111112"), new Guid("08111111-1111-1111-1111-111111111111"), "دمياط" },
                    { new Guid("81111111-1111-1111-1111-811111111113"), new Guid("08111111-1111-1111-1111-111111111111"), "رأس البر" },
                    { new Guid("81111111-1111-1111-1111-811111111114"), new Guid("08111111-1111-1111-1111-111111111111"), "كفر سعد" },
                    { new Guid("81111111-1111-1111-1111-811111111115"), new Guid("08111111-1111-1111-1111-111111111111"), "فارسكور" },
                    { new Guid("91111111-1111-1111-1111-911111111112"), new Guid("09111111-1111-1111-1111-111111111111"), "بنها" },
                    { new Guid("91111111-1111-1111-1111-911111111113"), new Guid("09111111-1111-1111-1111-111111111111"), "شبرا الخيمة" },
                    { new Guid("91111111-1111-1111-1111-911111111114"), new Guid("09111111-1111-1111-1111-111111111111"), "قليوب" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("10111111-1111-1111-1111-101111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("10111111-1111-1111-1111-101111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("10111111-1111-1111-1111-101111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("10111111-1111-1111-1111-101111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("10111111-1111-1111-1111-101111111116"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("21111111-1111-1111-1111-211111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("21111111-1111-1111-1111-211111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("21111111-1111-1111-1111-211111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("21111111-1111-1111-1111-211111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("31111111-1111-1111-1111-311111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("31111111-1111-1111-1111-311111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("31111111-1111-1111-1111-311111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("31111111-1111-1111-1111-311111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("41111111-1111-1111-1111-411111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("41111111-1111-1111-1111-411111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("41111111-1111-1111-1111-411111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("41111111-1111-1111-1111-411111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("41111111-1111-1111-1111-411111111116"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-511111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-511111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-511111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("51111111-1111-1111-1111-511111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111116"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111117"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("61111111-1111-1111-1111-611111111118"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("71111111-1111-1111-1111-711111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("71111111-1111-1111-1111-711111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("71111111-1111-1111-1111-711111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("71111111-1111-1111-1111-711111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("71111111-1111-1111-1111-711111111116"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("81111111-1111-1111-1111-811111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("81111111-1111-1111-1111-811111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("81111111-1111-1111-1111-811111111114"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("81111111-1111-1111-1111-811111111115"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("91111111-1111-1111-1111-911111111112"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("91111111-1111-1111-1111-911111111113"));

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: new Guid("91111111-1111-1111-1111-911111111114"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("01111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("02111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("03111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("04111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("05111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("06111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("07111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("08111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("09111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Governorates",
                keyColumn: "Id",
                keyValue: new Guid("10111111-1111-1111-1111-111111111111"));

        }
    }
}
