create database CINE
use CINE



create table [User](
    id integer NOT NULL primary key IDENTITY(1,1),
    "Email" nvarchar(255) NOT NULL,
    "Password" nvarchar(255) NOT NULL,
    "Name" nvarchar(255) NOT NULL,
    "Phone" text NOT NULL,
    "Gender" bit NOT NULL,
    "Birthday" datetime NOT NULL,
    "Permission" nvarchar(5) NOT NULL DEFAULT('User'),
);

insert into [User](Email,Password,Name,Phone,Gender,Birthday,Permission) values('phammanhbinh.it@gmail.com','123',N'Phạm Mạnh Bình','0928981048',1,'2000-01-08','Admin')

create table CumRap(
    "id" int NOT NULL primary key IDENTITY(1,1),
    "TenCum" nvarchar(255) NOT NULL,
    "DiaChi" nvarchar(255) NOT NULL,
    "Maps" text NOT NULL,
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
);

create table Rap(
    "id" integer NOT NULL primary key IDENTITY(1,1),
    "TenRap" nvarchar(255) NOT NULL,
    "LoaiRap" nvarchar(255) NOT NULL,
    "KTNgang" integer NOT NULL,
    "KTDoc" integer NOT NULL,
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
    "CumRapId" integer foreign key references CumRap(id),
);

create table Ghe(
	"id" integer NOT NULL primary key IDENTITY(1,1),
	"idRap" integer foreign key references Rap(id) NOT NULL,
	"Hang" char(1) NOT NULL,
	"SoGhe" integer NOT NULL,
	/*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
);

create table Phim(
    "id" integer NOT NULL primary key IDENTITY(1,1),
    "Ten" nvarchar(255) NOT NULL,
    "Poster" nvarchar(255),
    "TraiLer" nvarchar(255) NOT NULL,
    "ThoiLuong" integer NOT NULL,
    "DaoDien" nvarchar(255) NOT NULL,
    "DienVien" nvarchar(255) NOT NULL,
    "TheLoai" nvarchar(255) NOT NULL,
	"NoiDungPhim" nvarchar(3000),
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
    "NgayCongChieu" date,
);

create table SuatChieu(
    "id" integer NOT NULL primary key IDENTITY(1,1),
	"Ngay" date,
    "ThoiDiemBatDau" datetime,
    "ThoiDiemKetThuc" datetime,
    "GiaVe" integer,
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
    "PhimId" integer foreign key references Phim(id),
    "RapId" integer foreign key references Rap(id)
);

create table FastFood(
	"id" integer NOT NULL primary key IDENTITY(1,1),
	"TenMon" nvarchar(255) NOT NULL,
	"MoTa" nvarchar(500),
	"Gia" float NOT NULL,
	"Image" text,
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
);

create table Ve(
    "MaVe" int NOT NULL primary key IDENTITY(1,1),
	"SuatChieuId" integer NOT NULL foreign key references SuatChieu(id),
    "UserId" integer foreign key references [User](id) NOT NULL,
    "ThoiDiemDatVe" datetime NOT NULL,
        /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
    TongTien float,
);

create table ChiTietVe(
    "MaVe" int NOT NULL foreign key references Ve(MaVe),
    "MaGhe" integer NOT NULL foreign key references Ghe(id),
    "ThanhTien" float NOT NULL,
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
    primary key(MaVe,MaGhe)
);

 create table ChiTietVe_Food(
	"MaVe" int NOT NULL foreign key references Ve(MaVe),
	"FoodId" integer foreign key references FastFood(id),
    "SoLuong" integer,
    "ThanhTien" float NOT NULL,
    /*"createdAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),
    "updatedAt" DATETIME2(3) NOT NULL DEFAULT (SYSDATETIME()),*/
    primary key(MaVe,FoodId)
 );
 
 create table Banner(
	"id" int NOT NULL primary key IDENTITY(1,1),
	"Image" text,
	"Link" text,
 );

//triger tạo dữ liệu tự động cho bảng Ghế ghi tạo hoặc update bảng Rạp
create trigger CreateSeat on Rap
after insert,update as
begin
	declare @ktngang int;
	declare @ktdoc int;
	declare @idrap int;
	set @ktngang=(select KTNgang from inserted);
	set @ktdoc=(select KTDoc from inserted);
	set @idrap=(select id from inserted);
	
	declare @i int;
	set @i=0;
	declare @j int;
	set @j=1;
	
	while @i < @ktdoc
	begin
		set @j=1;
		while @j <= @ktngang
		begin
			insert into Ghe(idRap,Hang,SoGhe) values(@idrap,CHAR(@i+65),@j);
			set @j=@j+1;
		end;
	print @i;
	set @i=@i+1;
	end;
end;

create trigger UpdateTongTien_1 on ChiTietVe
after insert,update as
begin
	update Ve
	set TongTien=TongTien+(select ThanhTien from inserted)
	from inserted
	where Ve.MaVe=inserted.MaVe;
end

create trigger UpdateTongTien_2 on ChiTietVe_Food
after insert,update as
begin
	update Ve
	set TongTien=TongTien+(select ThanhTien from inserted)
	from inserted
	where Ve.MaVe=inserted.MaVe;
end


create trigger CheckVe on ChiTietVe
after insert,update as
begin
	DECLARE @T INT
	SET @T = (select COUNT(*) from Ve,ChiTietVe,inserted where ChiTietVe.MaGhe=inserted.MaGhe and Ve.SuatChieuId and Ve.MaVe=ChiTietVe.MaVe and inserted.MaVe=Ve.MaVe)
	IF(@T>0)
		rollback tran
end
