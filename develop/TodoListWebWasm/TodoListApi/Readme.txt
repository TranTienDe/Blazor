## Thêm thư viên cho Entityframeworkcore
1. Microsoft.EntityFrameworkCore
2. Microsoft.EntityFrameworkCore.Design
3. Microsoft.EntityFrameworkCore.SqlServer
4. Microsoft.EntityFrameworkCore.Tools

## Migration
1. Add-Migration
2. Update-database


## Thêm ASP.Net Core identity chứa bảng user, role...
1. Microsoft.AspNetCore.Identity.EntityFrameworkCore

## TEDU Series
https://www.youtube.com/watch?v=kJ5--_kpMoo&list=PLRhlTlpDUWszBRs388Koxf6sMBSuOs8cd&index=31

## Bài 25: Hiển thị Toast message
Package tham khảo: https://github.com/Blazored/toast

Bước 1: Install from visual studio
	Install-Package Blazored.Toast

Bước 2: Add trong program.cs
	builder.Services.AddBlazoredToast();

Bước 3: Add the following to your _Imports.razor
	@using Blazored.Toast
	@using Blazored.Toast.Services

Bước 4: Add the <BlazoredToasts /> tag into your applications MainLayout.razor.
<BlazoredToasts Position="ToastPosition.BottomRight"
                Timeout="10"
                IconType="IconType.FontAwesome"
                SuccessClass="success-toast-override"
                SuccessIcon="fa fa-thumbs-up"
                ErrorIcon="fa fa-bug" />


Bước 5: Add reference to style sheet(s) trong index.html
<link href="_content/Blazored.Toast/blazored-toast.min.css" rel="stylesheet" />

Bước 6: Đưa vào trong trang sử dụng
@inject IToastService toastService

<button class="btn btn-info" @onclick="@(() => toastService.ShowInfo("I'm an INFO message"))">Info Toast</button>


