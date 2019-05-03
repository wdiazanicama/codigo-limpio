using BusinessLayer;

namespace DataAccessLayer
{
    public class SqlServerCompactRepository : IRepository
	{
        /// <summary>
        /// Save speaker to DB for now. For demo, just assume success and return 1.
        /// </summary>
        /// <param name="speaker"></param>
        /// <returns></returns>
        public int SaveSpeaker(Speaker speaker) => 1;
	}
}
