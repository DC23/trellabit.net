using Newtonsoft.Json;
using Trellabit.Services.Habitica.Converters;
using Xunit;
using Trellabit.Services.Habitica.Model;
using Moq;
using System;

namespace Trellabit.Tests.Services.Habitica.Converters
{
    public class TestTaskConverter
    {
        [Fact]
        public void TestReadHabit()
        {
            string guid = Guid.NewGuid().ToString();
            string text = "bad habits";
            string type = "habit";
            string json = $@"{{
                'id': '{guid}',
                'text': '{text}',
                'type': '{type}',
                'up': 'false',
                'down': 'true',
            }}";

            var task = JsonConvert.DeserializeObject<Habit>(json, new TaskConverter());

            Assert.IsType<Habit>(task);
            Assert.Equal(text, task.Text);
            Assert.Equal(guid, task.Id.ToString());
            Assert.Equal(type, task.Type);
            Assert.False(task.Up);
        }

        [Fact]
        public void TestReadDaily()
        {
            string guid = Guid.NewGuid().ToString();
            string text = "daily is as daily does";
            string type = "daily";
            string notes = "these are some extra notes";
            string json = $@"{{
                'id': '{guid}',
                'text': '{text}',
                'type': '{type}',
                'notes': '{notes}',
                'streak': '42',
            }}";

            var task = JsonConvert.DeserializeObject<Daily>(json, new TaskConverter());

            Assert.IsType<Daily>(task);
            Assert.Equal(text, task.Text);
            Assert.Equal(guid, task.Id.ToString());
            Assert.Equal(type, task.Type);
            Assert.Equal(42, task.Streak);
            Assert.Equal(notes, task.Notes);
        }

        [Fact]
        public void TestReadTodo()
        {
            string guid = Guid.NewGuid().ToString();
            string text = "todo or not todo";
            string type = "todo";
            string json = $@"{{
                'id': '{guid}',
                'text': '{text}',
                'type': '{type}',
                'completed': 'true',
            }}";

            var task = JsonConvert.DeserializeObject<Todo>(json, new TaskConverter());

            Assert.IsType<Todo>(task);
            Assert.Equal(text, task.Text);
            Assert.Equal(guid, task.Id.ToString());
            Assert.Equal(type, task.Type);
            Assert.True(task.Completed);
        }

        [Fact]
        public void TestReadReward()
        {
            string guid = Guid.NewGuid().ToString();
            string text = "rewards rewards hmm";
            string type = "reward";
            string json = $@"{{
                'id': '{guid}',
                'text': '{text}',
                'type': '{type}',
            }}";

            var task = JsonConvert.DeserializeObject<Task>(json, new TaskConverter());

            Assert.IsType<Reward>(task);
            Assert.Equal(text, task.Text);
            Assert.Equal(guid, task.Id.ToString());
            Assert.Equal(type, task.Type);
        }
    }
}
