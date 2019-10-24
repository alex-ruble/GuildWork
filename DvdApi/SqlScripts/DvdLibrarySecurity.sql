create login DVDLibraryApp with password = 'testing123'
go

use DvdLibraryApp
go

create user DvdLibraryApp for login DVDLibraryApp
go

grant execute on GetAll to DvdLibraryApp
grant execute on GetOne to DvdLibraryApp
grant execute on Edit to DvdLibraryApp
grant execute on [Delete] to DvdLibraryApp
go

grant execute to DVDLibraryApp

grant select on DVDs to DVDLibraryApp
grant insert on DVDs to DVDLibraryApp
grant update on DVDs to DVDLibraryApp
grant delete on DVDs to DVDLibraryApp
go

grant execute on [Create] to DVDLibraryApp
go