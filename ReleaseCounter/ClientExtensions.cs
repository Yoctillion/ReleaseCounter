using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Octokit;
using ReleaseCounter.Models;

namespace ReleaseCounter
{
    public static class ClientExtensions
    {
        public static async Task<IEnumerable<RepositoryData>> GetRepositories(this GitHubClient client, string owner)
        {
            var repositories = await client.Repository.GetAllForUser(owner);
            return repositories.Select(r => new RepositoryData
            {
                Author = r.Owner.Login,
                Name = r.Name
            });
        }

        public static async Task<RepositoryData> GetRepository(this GitHubClient client, string owner, string repoName)
        {
            var releases = await client.GetReleases(owner, repoName);

            var repoInfo = new RepositoryData
            {
                Author = owner,
                Name = repoName,
                Releases = releases.ToArray()
            };

            repoInfo.DownloadCount = repoInfo.Releases.Sum(r => r.Downloads.Sum(d => d.DownloadCount));
            return repoInfo;
        }

        public static async Task<IEnumerable<ReleaseData>> GetReleases(this GitHubClient client, string owner, string repoName)
        {
            var releases = await client.Repository.Release.GetAll(owner, repoName);

            return releases.Select(r => new ReleaseData
            {
                ReleaseName = r.Name,
                TagName = r.TagName,

                ReleaseTime = r.PublishedAt?.AddHours(8).ToString("yyyy-MM-dd hh:mm") ?? "",

                Author = r.Author.Login,
                AuthorUrl = r.Author.HtmlUrl,

                Downloads = r.Assets.Select(d => new DownloadData
                {
                    FileName = d.Name,
                    Url = d.Url,
                    DownloadCount = d.DownloadCount
                })
                .ToArray()
            });
        }
    }
}
