#pragma checksum "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\Create.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a7b9936948a8d790dce0e2ef531b4d5ded6bb903"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Admin_Views_HoaDons_Create), @"mvc.1.0.view", @"/Areas/Admin/Views/HoaDons/Create.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a7b9936948a8d790dce0e2ef531b4d5ded6bb903", @"/Areas/Admin/Views/HoaDons/Create.cshtml")]
    public class Areas_Admin_Views_HoaDons_Create : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WebASP.Models.HoaDon>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\Create.cshtml"
  
    ViewData["Title"] = "Create";
    Layout = "~/Views/Shared/_LayoutAdmin.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Thêm mới hóa đơn</h1>
<hr />
<div class=""row"">
    <div class=""col-md-4"">
        <form asp-action=""Create"">
            <div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
            <div class=""form-group"">
                <label asp-for=""MAHD"" class=""control-label""></label>
                <input asp-for=""MAHD"" class=""form-control"" />
                <span asp-validation-for=""MAHD"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""TaiKhoan"" class=""control-label""></label>
                <select asp-for=""TaiKhoan"" class =""form-control"" asp-items=""ViewBag.TaiKhoanId""></select>
            </div>
            <div class=""form-group"">
                <label asp-for=""NgayLap"" class=""control-label""></label>
                <input asp-for=""NgayLap"" class=""form-control"" />
                <span asp-validation-for=""NgayLap"" class=""text-danger""></span>
            </div>
            <div class=""form-group form-");
            WriteLiteral("check\">\r\n                <label class=\"form-check-label\">\r\n                    <input class=\"form-check-input\" asp-for=\"ThanhToan\" /> ");
#nullable restore
#line 30 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\Create.cshtml"
                                                                      Write(Html.DisplayNameFor(model => model.ThanhToan));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </label>
            </div>
            <div class=""form-group"">
                <label asp-for=""DChiGiaoHang"" class=""control-label""></label>
                <input asp-for=""DChiGiaoHang"" class=""form-control"" />
                <span asp-validation-for=""DChiGiaoHang"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""SDTGiaoHang"" class=""control-label""></label>
                <input asp-for=""SDTGiaoHang"" class=""form-control"" />
                <span asp-validation-for=""SDTGiaoHang"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""TenNguoiNhan"" class=""control-label""></label>
                <input asp-for=""TenNguoiNhan"" class=""form-control"" />
                <span asp-validation-for=""TenNguoiNhan"" class=""text-danger""></span>
            </div>
            <div class=""form-group"">
                <label asp-for=""TongTien"" class=""control-label""><");
            WriteLiteral(@"/label>
                <input asp-for=""TongTien"" class=""form-control"" />
                <span asp-validation-for=""TongTien"" class=""text-danger""></span>
            </div>
            <div class=""form-group form-check"">
                <label class=""form-check-label"">
                    <input class=""form-check-input"" asp-for=""TrangThai"" /> ");
#nullable restore
#line 55 "D:\DoAn_HK5_2021_2022\WebASP\WebASP\Areas\Admin\Views\HoaDons\Create.cshtml"
                                                                      Write(Html.DisplayNameFor(model => model.TrangThai));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </label>
            </div>
            <div class=""form-group"">
                <input type=""submit"" value=""Thêm"" class=""btn btn-primary"" />
            </div>
        </form>
    </div>
</div>

<div>
    <a asp-action=""Index"">Trở về</a>
</div>

");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WebASP.Models.HoaDon> Html { get; private set; }
    }
}
#pragma warning restore 1591
