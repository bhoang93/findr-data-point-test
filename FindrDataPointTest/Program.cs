// See https://aka.ms/new-console-template for more information

using FindrDataPointTest.HttpClients;
using FindrDataPointTest.HttpClients.Stackoverflow;
using FindrDataPointTest.SkillGatherers;
using FindrDataPointTest.SkillGatherers.Stackoverflow;

IGithubHttpClient client = new GithubHttpClient();
var gatherer = new GithubSkillGatherer(client);
var skills = await gatherer.GetSkillsFromUser("bhoang93");
Console.WriteLine(skills.First());

var client1 = new StackoverflowHttpClient();
var gatherer1 = new StackoverflowSkillGatherer(client1);
var skillSet = await gatherer1.GetSkillsFromUser("13570600");
skillSet.ForEach(skill => Console.WriteLine(skill.language));

