using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NotVisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RefrectionPerformanceTest.data;
using System.Collections.Concurrent;

#pragma warning disable CS8618
namespace RefrectionPerformanceTest
{
    [MemoryDiagnoser]
    //[ShortRunJob]
    [MinColumn, MaxColumn]
    [IterationCount(3)]
    public class TypeCastMethodImplEvaluation
    {
        PropertyInfo[] sourceProperyinfoCache;
        PropertyInfo[] destProperyinfoCache;
        Dictionary<string, int> destPropertyIndex;

        List<PostCodeTable> evalData = new List<PostCodeTable>();


        public TypeCastMethodImplEvaluation()
        {
            Console.WriteLine($"{System.DateTime.Now.ToString()}:Cast Performance Test Start");
            evalData = DataReady();
            Console.WriteLine($"{System.DateTime.Now.ToString()}:Data Ready Complete");
        }

        public void TestRun()
        {
            var evalDatas = new List<PostCodeTable>();
            //datas.AddRange(data);
            for (int i = 0; i < 1; i++)
            {
                evalDatas.AddRange(evalData);
            }
            evalData = evalDatas;

            Console.WriteLine($"-----------------------------------------------------------------");
            RunDirectForEachLoop();
            RunDirectForEachLoop();
            RunDirectForEachLoop();
            RunDirectForEachLoop();
            RunDirectForEachLoop();

            Console.WriteLine($"-----------------------------------------------------------------");
            RunDirectUsingStaticMapperMethodForEach();
            RunDirectUsingStaticMapperMethodForEach();
            RunDirectUsingStaticMapperMethodForEach();
            RunDirectUsingStaticMapperMethodForEach();
            RunDirectUsingStaticMapperMethodForEach();

            Console.WriteLine($"-----------------------------------------------------------------");
            RunDirectUsingStaticMapperMethodLinq();
            RunDirectUsingStaticMapperMethodLinq();
            RunDirectUsingStaticMapperMethodLinq();
            RunDirectUsingStaticMapperMethodLinq();
            RunDirectUsingStaticMapperMethodLinq();

            Console.WriteLine($"-----------------------------------------------------------------");
            RunDirectLinqDDModelDefault();
            RunDirectLinqDDModelDefault();
            RunDirectLinqDDModelDefault();
            RunDirectLinqDDModelDefault();
            RunDirectLinqDDModelDefault();
            /*
            Console.WriteLine($"-----------------------------------------------------------------");
            RunCastForEachSafedMethod(datas);
            RunCastForEachSafedMethod(datas);
            RunCastForEachSafedMethod(datas);
            RunCastForEachSafedMethod(datas);
            RunCastForEachSafedMethod(datas);
            Console.WriteLine($"-----------------------------------------------------------------");

            RunCastForEachSafedMethodWithLinq(datas);

            RunCastForEachSafedMethodUsingCache(datas);
            RunCastForEachSafedMethodUsingCache(datas);
            RunCastForEachSafedMethodUsingCache(datas);
            RunCastForEachSafedMethodUsingCache(datas);
            RunCastForEachSafedMethodUsingCache(datas);

            Console.WriteLine($"-----------------------------------------------------------------");

            RunCastForEachSafedMethodInstanceArgs(datas);
            RunCastForEachSafedMethodInstanceArgs(datas);
            RunCastForEachSafedMethodInstanceArgs(datas);
            RunCastForEachSafedMethodInstanceArgs(datas);
            RunCastForEachSafedMethodInstanceArgs(datas);*/

            Console.WriteLine($"-----------------------------------------------------------------");

            RunCastForEachSafedMethodInstanceArgsPlusNameMapping_UnSafe();
            RunCastForEachSafedMethodInstanceArgsPlusNameMapping_UnSafe();
            RunCastForEachSafedMethodInstanceArgsPlusNameMapping_UnSafe();
            RunCastForEachSafedMethodInstanceArgsPlusNameMapping_UnSafe();
            RunCastForEachSafedMethodInstanceArgsPlusNameMapping_UnSafe();

            Console.WriteLine($"-----------------------------------------------------------------");

            RunCastUsingArrayIndexCache();
            RunCastUsingArrayIndexCache();
            RunCastUsingArrayIndexCache();
            RunCastUsingArrayIndexCache();
            RunCastUsingArrayIndexCache();

        }


        ///[Benchmark]
        public stopWatch RunDirectForEachLoop()
        {
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                PostCodeJson json = new PostCodeJson()
                {
                    Position = row.Position,
                    Post5 = row.Post5,
                    Post7 = row.Post7,
                    prefkana = row.prefkana,
                    citykana = row.citykana,
                    townkana = row.townkana,
                    pref = row.pref,
                    city = row.city,
                    town = row.town,
                    kbn1 = row.kbn1,
                    kbn2 = row.kbn2,
                    kbn3 = row.kbn3,
                    kbn4 = row.kbn4,
                    kbn5 = row.kbn5,
                    kbn6 = row.kbn6
                };
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}:Direct RunDirectForEachLoop Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} millisec");
            return sw;
        }



        [Benchmark]
        public stopWatch RunDirectLinqDDModelDefault()
        {
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            result = evalData.Select(row => new PostCodeJson
            {
                Position = row.Position,
                Post5 = row.Post5,
                Post7 = row.Post7,
                prefkana = row.prefkana,
                citykana = row.citykana,
                townkana = row.townkana,
                pref = row.pref,
                city = row.city,
                town = row.town,
                kbn1 = row.kbn1,
                kbn2 = row.kbn2,
                kbn3 = row.kbn3,
                kbn4 = row.kbn4,
                kbn5 = row.kbn5,
                kbn6 = row.kbn6
            }).ToList();
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}:Direct RunDirectLinqDDModelDefault RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} millisec");
            return sw;
        }

        ///[Benchmark]
        public stopWatch RunDirectUsingStaticMapperMethodForEach()
        {
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                var json = (PostCodeJson)PostCodeTableConverter.ToJson(row);
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}:Direct RunDirectUsingStaticMapperMethodForEach Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} millisec");
            return sw;
        }

        [Benchmark]
        public stopWatch RunDirectUsingStaticMapperMethodLinq()
        {
            //Test1 Direct Element Mapping
            var sw = new stopWatch();
            sw.Start();
            var result = evalData.Select(x => (PostCodeJson)PostCodeTableConverter.ToJson(x)).ToList();
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: Direct RunDirectUsingStaticMapperMethodLinq Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} millisec");
            return sw;
        }

        [Benchmark]
        public stopWatch RunDirectUsingStaticMapperMethodLinqParallel()
        {
            var sw = new stopWatch();
            sw.Start();
            var result = new ConcurrentBag<(int Index, PostCodeJson Json)>();
            var mapper = new data.PostCodeTableConverterImpl();
            Parallel.ForEach(evalData.Select((x, index) => new { x, index }), item =>
            {
                var json = (PostCodeJson)mapper.ToJson(item.x);
                result.Add((item.index, json));
            });
            // Convert ConcurrentBag to List and sort by index
            var resultList = result.OrderBy(x => x.Index).Select(x => x.Json).ToList();

            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: Direct RunDirectUsingStaticMapperMethodLinq Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} millisec");
            return sw;
        }

        [Benchmark]
        public stopWatch RunDirectUsingMapperMethodLinq()
        {
            var mapper = new data.PostCodeTableConverterImpl();
            //Test1 Direct Element Mapping
            var sw = new stopWatch();
            sw.Start();
            var result = evalData.Select(x => mapper.ToJson(x)).ToList();
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: Direct RunDirectUsingStaticMapperMethodLinq Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} millisec");
            return sw;
        }


        ///[Benchmark]
        public stopWatch RunCastForEachSafedMethod()
        {
            string summary = "安全にRefrectionを利用したダウンキャストを行う";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                PostCodeJson? json = DownCast<PostCodeJson>(row);
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} RunCastForEachSafedMethod Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }

        ///[Benchmark]
        public stopWatch RunCastForEachSafedMethodWithLinq()
        {
            string summary = "安全にRefrectionを利用したダウンキャストを行う(Linq)";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            result = evalData.Select(x => DownCast<PostCodeJson>(x)).ToList();
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} RunCastForEachSafedMethod Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }
        ///[Benchmark]
        public stopWatch RunCastForEachSafedMethodWithLinqStatic()
        {
            string summary = "安全にRefrectionを利用したダウンキャストを行う(Linq+StaticMethod)";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            result = evalData.Select(x => DownCastStatic<PostCodeJson>(x)).ToList();
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} RunCastForEachSafedMethod Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }

        ///[Benchmark]
        public stopWatch RunCastForEachSafedMethodUsingCache()
        {
            string summary = "安全にRefrectionを利用したダウンキャストを行う(PropertyInfoをキャッシュしてコード実行頻度を減らす)";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                PostCodeJson? json = DownCastWithCache<PostCodeJson>(row);
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} DownCast Using DownCastWithCache Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }

        ///[Benchmark]
        public stopWatch RunCastForEachSafedMethodInstanceArgs()
        {
            string summary = "型明示かつインスタンスを渡して型推論、インスタンス生成を省略";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                PostCodeJson json = new PostCodeJson();
                DownCastAtoB(row, json);
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} DownCastAtoB Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }

        ///[Benchmark]
        public stopWatch RunCastForEachSafedMethodInstanceArgsPlusNameMapping_UnSafe()
        {
            string summary = "型明示かつインスタンスを渡して型推論、インスタンス生成を省略に加え、名前だけで値複写";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                PostCodeJson json = new PostCodeJson();
                DownCastAtoBUnSafe(row, json);
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} Downcast DownCastAtoBUnSafe Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }

        ///[Benchmark]
        public stopWatch RunCastUsingArrayIndexCache()
        {
            string summary = "indexキャッシュを利用したRefrection Unsafe";
            //Test1 Direct Element Mapping
            List<PostCodeJson> result = new List<PostCodeJson>();
            var sw = new stopWatch();
            sw.Start();
            foreach (var row in evalData)
            {
                PostCodeJson json = new PostCodeJson();
                DownCastAtoBUnSafeUsingPropertyInfoIndex(row, json);
                result.Add(json);
            }
            sw.Stop();
            Console.WriteLine($"{System.DateTime.Now.ToString()}: {summary} Downcast DownCastAtoBUnSafeUsingPropertyInfoIndex Complete RecordCount = {evalData.Count()}");
            Console.WriteLine($"Prosessing TotalSeconds : {sw.ElapsedMilliseconds.ToString()} milliSec");
            return sw;
        }

        public T DownCast<T>(object source)
        {
            Type sourceType = source.GetType();
            Type destType = typeof(T);
            var dest = Activator.CreateInstance(typeof(T));

            var source_pis = sourceType.GetProperties();
            var dest_pis = destType.GetProperties();

            if (dest != null)
            {
                foreach (PropertyInfo pi in source_pis)
                {
                    var dest_pi = dest_pis.Where(x => x.Name == pi.Name && x.PropertyType == pi.PropertyType && x.CanWrite).FirstOrDefault();
                    if (dest_pi != null)
                    {
                        dest_pi.SetValue((T)dest, pi.GetValue(source));
                    }
                }
                return (T)dest;
            }
            else
            {
                return default(T);
            }
        }

        public static T DownCastStatic<T>(object source)
        {
            Type sourceType = source.GetType();
            Type destType = typeof(T);
            var dest = Activator.CreateInstance(typeof(T));

            var source_pis = sourceType.GetProperties();
            var dest_pis = destType.GetProperties();

            if (dest != null)
            {
                foreach (PropertyInfo pi in source_pis)
                {
                    var dest_pi = dest_pis.Where(x => x.Name == pi.Name && x.PropertyType == pi.PropertyType && x.CanWrite).FirstOrDefault();
                    if (dest_pi != null)
                    {
                        dest_pi.SetValue((T)dest, pi.GetValue(source));
                    }
                }
                return (T)dest;
            }
            else
            {
                return default(T);
            }
        }

        public T DownCastWithCache<T>(object source)
        {
            Type sourceType;
            Type destType;
            PropertyInfo[] source_pis;
            PropertyInfo[] dest_pis;

            if (sourceProperyinfoCache == null)
            {
                sourceType = source.GetType();
                destType = typeof(T);
                source_pis = sourceType.GetProperties();
                dest_pis = destType.GetProperties();
            }
            else
            {
                source_pis = sourceProperyinfoCache;
                dest_pis = destProperyinfoCache;
            }
            var dest = Activator.CreateInstance(typeof(T));

            if (dest != null)
            {
                foreach (PropertyInfo pi in source_pis)
                {
                    var dest_pi = dest_pis.Where(x => x.Name == pi.Name && x.PropertyType == pi.PropertyType && x.CanWrite).FirstOrDefault();
                    if (dest_pi != null)
                    {
                        dest_pi.SetValue((T)dest, pi.GetValue(source));
                    }
                }
                return (T)dest;
            }
            else
            {
                return default(T);
            }
        }

        public void DownCastAtoB(PostCodeTable s, PostCodeJson d)
        {
            var source_pis = s.GetType().GetProperties();
            var dest_pis = d.GetType().GetProperties();
            foreach (PropertyInfo pi in source_pis)
            {
                var dest_pi = dest_pis.Where(x => x.Name == pi.Name && x.PropertyType == pi.PropertyType && x.CanWrite).FirstOrDefault();
                if (dest_pi != null)
                {
                    dest_pi.SetValue(d, pi.GetValue(s));
                }
            }
        }

        public void DownCastAtoBUnSafe(PostCodeTable s, PostCodeJson d)
        {
            var source_pis = s.GetType().GetProperties();
            var dest_pis = d.GetType().GetProperties();
            foreach (PropertyInfo pi in source_pis)
            {
                var dest_pi = Array.Find(dest_pis, x => x.Name == pi.Name);
                if (dest_pi != null)
                {
                    dest_pi.SetValue(d, pi.GetValue(s));
                }
            }
        }

        public void DownCastAtoBUnSafeUsingPropertyInfoIndex(PostCodeTable s, PostCodeJson d)
        {
            if (sourceProperyinfoCache == null)
            {
                sourceProperyinfoCache = s.GetType().GetProperties();
                destProperyinfoCache = d.GetType().GetProperties();
                destPropertyIndex = new Dictionary<string, int>();
                foreach (PropertyInfo pi in sourceProperyinfoCache)
                {
                    for (int j = 0; j < destProperyinfoCache.Count(); j++)
                    {
                        if (destProperyinfoCache[j].Name == pi.Name &&
                            destProperyinfoCache[j].PropertyType == pi.PropertyType &&
                            destProperyinfoCache[j].CanWrite == true)
                        {
                            destPropertyIndex.Add(pi.Name, j);
                        }
                    }
                }
            }

            foreach (PropertyInfo pi in sourceProperyinfoCache)
            {
                destProperyinfoCache[destPropertyIndex[pi.Name]].SetValue(d, pi.GetValue(s));
            }
        }

        #region DataReady

        private List<PostCodeTable> DataReady()
        {
            using (StreamReader sr = new StreamReader(System.IO.Path.Combine(AppContext.BaseDirectory, @"data\utf_ken_all.csv")))
            {
                List<string[]> datas = new List<string[]>();
                List<PostCodeTable> recs = new List<PostCodeTable>();
                var csv = new CsvTextFieldParser(sr);
                while (true)
                {
                    var rec = csv.ReadFields();
                    if (csv.EndOfData)
                    {
                        break;
                    }
                    datas.Add(rec);
                }

                foreach (var r in datas)
                {
                    int pos = 0;
                    PostCodeTable j = new PostCodeTable();
                    j.Position = r[pos]; pos++;
                    j.Post5 = r[pos]; pos++;
                    j.Post7 = r[pos]; pos++;
                    j.prefkana = r[pos]; pos++;
                    j.citykana = r[pos]; pos++;
                    j.townkana = r[pos]; pos++;
                    j.pref = r[pos]; pos++;
                    j.city = r[pos]; pos++;
                    j.town = r[pos]; pos++;
                    j.kbn1 = r[pos]; pos++;
                    j.kbn2 = r[pos]; pos++;
                    j.kbn3 = r[pos]; pos++;
                    j.kbn4 = r[pos]; pos++;
                    j.kbn5 = r[pos]; pos++;
                    j.kbn6 = r[pos]; pos++;
                    recs.Add(j);
                }
                return recs;
            }
        }

        #endregion


        #region StopWatch


        public class stopWatch : System.Diagnostics.Stopwatch
        {
            private DateTime StartDateTime { get; set; }
            private DateTime StopDateTime { get; set; }
            public new void Start()
            {
                base.Start();
                StartDateTime = System.DateTime.Now;
            }

            public new void Stop()
            {
                base.Stop();
                StopDateTime = System.DateTime.Now;
            }
            public TimeSpan TimeSpan
            {
                get
                {
                    return StopDateTime - StartDateTime;
                }
            }
        }

        #endregion



    }
}
#pragma warning restore CS8618
