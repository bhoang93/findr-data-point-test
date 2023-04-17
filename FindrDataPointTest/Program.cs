// See https://aka.ms/new-console-template for more information

using FindrDataPointTest.HttpClients;
using FindrDataPointTest.SkillGatherers;

IGithubHttpClient client = new GithubHttpClient();
var gatherer = new GithubSkillGatherer(client);
var skills = await gatherer.GetSkillsFromUser("bhoang93");
Console.WriteLine(skills.First());