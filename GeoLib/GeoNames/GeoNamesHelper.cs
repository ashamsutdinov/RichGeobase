using System;
using System.Collections.Generic;
using System.Linq;
using GeoLib.Helpers;
using NGeo.GeoNames;

namespace GeoLib.GeoNames
{
    public static class GeoNamesHelper
    {
        public const string GeoNamesAccountsRaw = "orakzai,aetna,demo,aydar,aydar1,aydar2,aydar3,aydar4,aydar5,ngeo";

        private static readonly List<string> GeoNamesAccounts;

        private static string _currentGeoNamesAccount;

        static GeoNamesHelper()
        {
            GeoNamesAccounts = GeoNamesAccountsRaw.Split(new[] {','}, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private static GeoNamesClient _client;

        public static Toponym GetToponym(int id)
        {
            var requested = RequestFromGeoNames(c => c.Get(id, _currentGeoNamesAccount));
            var tries = 0;
            while (requested == null && tries < 10)
            {
                requested = RequestFromGeoNames(c => c.Get(id, _currentGeoNamesAccount));
                tries++;
            }
            return requested;
        }

        public static TResult RequestFromGeoNames<TResult>(Func<GeoNamesClient, TResult> request)
            where TResult : class
        {
            if (_client == null)
            {
                var acc = GeoNamesAccounts.Shuffle().FirstOrDefault();
                _currentGeoNamesAccount = acc;
                _client = new GeoNamesClient();
            }
            var result = request(_client);
            if (result == null)
            {
                _client.Abort();
                _client.Close();
                _client = null;
            }
            return result;
        }
    }
}
