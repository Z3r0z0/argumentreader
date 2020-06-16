using System;
using System.Collections.Generic;
using System.Text;

namespace ArgumentMarshalerLib
{
    public class Iterator<T>
    {
        private readonly IList<T> _list;
        private int _index;

        public Iterator(IList<T> list)
        {
            if (list == null)
                throw new NullReferenceException();

            this._list = list;
        }

        public T Current
        {
            get => _list[_index];
        }

        public bool HasNext => _index < _list.Count;

        public T Next()
        {
            return _list[_index++];
        }

        public T Previous()
        {
            return _list[--_index];
        }
    }
}
