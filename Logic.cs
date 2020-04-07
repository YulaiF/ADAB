using System;
using System.Collections.Generic;
using System.Linq;

namespace ADAB
{
    /// <summary>
    /// Основная логика взаимодействия
    /// </summary>
    public class Logic
    {
        public static readonly Connect_Item zeroConnectItem = new Connect_Item("000000000");

        /// <summary>
        /// Адрес подключения
        /// </summary>
        public class Connect_Item
        {
            public readonly string adAlias = "";
            public readonly string ID = "";
            public readonly string Name = "";
            public readonly string Comment = "";

            /// <summary>
            /// Конструктор строки коннекта
            /// </summary>
            /// <param name="iD">ID удалённого компьютера</param>
            /// <param name="adAlias">Псевдоним формата </param>
            /// <param name="name">Понятное имя</param>
            /// <param name="comment">В избранном  или нет</param>
            public Connect_Item(string iD, string adAlias = "", string name = "", string comment = "")
            {
                ID = iD ?? throw new ArgumentNullException(nameof(iD));
                this.adAlias = adAlias == "" ? ID : adAlias;
                Name = name ?? "";
                this.Comment = comment ?? "";
                return;
            }

            public Connect_Item(string connectionString)
            {
                var splitString = connectionString.Split(',');
                try
                {
                    if (splitString.Count<string>() > 1)
                    {
                        ID = splitString[1];
                        adAlias = splitString[0] ?? splitString[1];
                        Name = splitString[2] ?? "";
                        Comment = splitString[3] ?? "";
                    }
                    else
                    {
                        ID = splitString[0] ?? splitString[1];
                        adAlias = splitString[0] ?? splitString[1];
                        Name = "" ?? splitString[2];
                        Comment = "" ?? splitString[3];
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                return;
            }

            /// <summary>
            /// Алиас или Имя
            /// </summary>
            /// <returns></returns>
            public override string ToString()
            {
                //Строка вида "adAlias,ID,Name,"fav""
                //return this.adAlias + "," + this.ID + "," + this.Name + "," + this.Favorite;
                return this.Name == "" ? this.adAlias : this.Name;
            }

            public override bool Equals(object obj)
            {
                return obj is Connect_Item item &&
                       adAlias == item.adAlias &&
                       ID == item.ID &&
                       Name == item.Name &&
                       Comment == item.Comment;
            }

            public override int GetHashCode()
            {
                int hashCode = -1201544425;
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(adAlias);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ID);
                hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
                hashCode = hashCode * -1521134295 + Comment.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// Книга адресов
        /// </summary>
        public class BookItem
        {
            public readonly Guid BookGUID = Guid.Empty;
            public readonly string BookName = "";
            public readonly string BookCreationDate = "";

            public BookItem(string bookName)
            {
                BookName = bookName ?? throw new ArgumentNullException(nameof(bookName));
            }

            public BookItem(Guid bookGUID, string bookName)
            {
                BookGUID = bookGUID;
                BookName = bookName ?? throw new ArgumentNullException(nameof(bookName));
            }

            public BookItem(Guid bookGUID, string bookName, string bookCreationDate) : this(bookGUID, bookName)
            {
                BookCreationDate = bookCreationDate ?? throw new ArgumentNullException(nameof(bookCreationDate));
            }

            public override string ToString()
            {
                return BookName;
            }

            public static bool operator !=(BookItem bookA, BookItem bookB)
            {
                return !(bookA == bookB);
            }
            public static bool operator ==(BookItem bookA, BookItem bookB)
            {
                return bookA.BookName == bookB.BookName && bookA.BookGUID == bookB.BookGUID && bookA.BookCreationDate == bookB.BookCreationDate;
            }

            public override bool Equals(object obj)
            {
                return base.Equals(obj);
            }

            public override int GetHashCode()
            {
                return base.GetHashCode();
            }
        }
    }
}
