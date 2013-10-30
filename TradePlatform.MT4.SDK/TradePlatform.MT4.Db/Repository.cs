﻿using System.Collections.Generic;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using TradePlatform.MT4.Db.Entities;
using TradePlatform.MT4.Db.Mappings;

namespace TradePlatform.MT4.Db
{
    public class Repository<T> : IRepository<T>
    {
        protected ISessionFactory _sessionFactory;
        public IList<LineBalanceAdvisorDetails> Items;

        public Repository()
        {
            var serverName = System.Configuration.ConfigurationManager.AppSettings["Server"];
            var dbName = System.Configuration.ConfigurationManager.AppSettings["DataBase"];
            var userName = System.Configuration.ConfigurationManager.AppSettings["UserName"];
            var pwd = System.Configuration.ConfigurationManager.AppSettings["Password"];

            _sessionFactory = Fluently.Configure().Database(MySQLConfiguration.Standard
                                       .ConnectionString(c => c
                                       .Server(serverName)
                                       .Database(dbName)
                                       .Username(userName)
                                       .Password(pwd)))
                                       .Mappings(m => m.FluentMappings.AddFromAssemblyOf<LineBalanceAdvisorDetailsMap>())
                                       .BuildSessionFactory();

            GetUpdatedDbContent();
        }

        private void GetUpdatedDbContent()
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                Items = (IList<LineBalanceAdvisorDetails>) session.CreateCriteria(typeof(T)).List<T>();
                transaction.Commit();
            }
        }


        public void Insert(T entity)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Save(entity);
                transaction.Commit();
            }
            GetUpdatedDbContent();
        }

        public virtual void Update(T entity)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                session.Update(entity);
                //transaction.Commit();
            }
        }

        public T GetById(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            using (var transaction = session.BeginTransaction())
            {
                var result = session.Get<T>(id);
                transaction.Commit();
                return result;
            }
        }
    }
}
