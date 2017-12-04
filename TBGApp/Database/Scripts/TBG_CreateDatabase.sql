if (not exists(select 1 from master.dbo.sysdatabases where name = 'TBG')) begin
	create database TBG
end