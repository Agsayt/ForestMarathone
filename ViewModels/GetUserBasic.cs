using ForestMarathone.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ForestMarathone.ViewModels
{
    // INotifyPropertyChanged notifies the View of property changes, so that Bindings are updated.
    sealed class GetUserBasic : INotifyPropertyChanged
    {
        ForestMarathoneEntities ctx = new ForestMarathoneEntities();

        public GetUserBasic()
        {
            FillAuthors();
        }

        public void FillAuthors()
        {
            Users = new List<UserBasic>();

            var q = (from a in ctx.Users where a.RoleId != Role.Administrator
                     select a).ToList();
            foreach (var usr in q)
            {                
                var user = new UserBasic
                {
                    Id = usr.UserId,
                    Login = usr.Login,
                    Password = usr.Password,
                    RoleId = usr.RoleId,
                    email = usr.Email,
                    RoleName = usr.Roles.Title
                };
                Users.Add(user);
            }
        }

        private List<UserBasic> _users;
        public List<UserBasic> Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
                NotifyPropertyChanged();
            }
        }

        //private Users _selectedAuthor;
        //public Users SelectedAuthor
        //{
        //    get
        //    {
        //        return _selectedAuthor;
        //    }
        //    set
        //    {
        //        _selectedAuthor = value;
        //        NotifyPropertyChanged();
        //        //FillBook();
        //    }
        //}

        //private void FillBook()
        //{
        //    Author author = this.SelectedAuthor;

        //    var q = (from book in ctx.Books
        //             orderby book.Title
        //             where book.AuthorId == author.AuthorId
        //             select book).ToList();

        //    this.Books = q;
        //}

        //private List<Book> _books;
        //public List<Book> Books
        //{
        //    get
        //    {
        //        return _books;
        //    }
        //    set
        //    {
        //        _books = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        //private Book _selectedBook;
        //public Book SelectedBook
        //{
        //    get
        //    {
        //        return _selectedBook;
        //    }
        //    set
        //    {
        //        _selectedBook = value;
        //        NotifyPropertyChanged();
        //    }
        //}

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "Users")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
