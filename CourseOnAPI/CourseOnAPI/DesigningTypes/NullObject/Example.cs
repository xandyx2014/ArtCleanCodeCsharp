using System.Web;

namespace DesigningTypes.NullObject {
    public interface ICacheStorage {
        void Remove(string key);
        void Store(string key, object data);
        T Retrieve<T>(string key);
    }

    public class HttpContextCacheStorage : ICacheStorage {
        public void Remove(string key) {
            HttpContext.Current.Cache.Remove(key);
        }

        public void Store(string key, object data) {
            HttpContext.Current.Cache.Insert(key, data);
        }

        public T Retrieve<T>(string key) {
            T itemsStored = (T) HttpContext.Current.Cache.Get(key);
            if (itemsStored == null) {
                itemsStored = default(T);
            }
            return itemsStored;
        }
    }

    public class NullObjectCache : ICacheStorage {
        public void Remove(string key) {}

        public void Store(string key, object data) {}

        public T Retrieve<T>(string key) {
            return default(T);
        }
    }

    public class Client {
        public static void PerformWork(ICacheStorage cacheStorage) {
            string key = "key";
            object o = cacheStorage.Retrieve<object>(key);
            if (o == null) {
                //simulate database lookup
                o = new object();
                cacheStorage.Store(key, o);
            }
            //perform some work on object o...
        }
    }
}