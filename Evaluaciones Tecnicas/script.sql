BACKUP DATABASE [evalucionesTecnicas]
   TO  DISK = N'C:\Auxiliar\Diferencial.bak'
   WITH DIFFERENTIAL;

   BACKUP LOG [AdventureWorks2012]
   TO  DISK = N'C:\Auxiliar\Diferencial.log';