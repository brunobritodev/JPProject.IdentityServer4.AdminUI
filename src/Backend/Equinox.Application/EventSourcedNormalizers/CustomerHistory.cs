using System;
using System.Collections.Generic;
using System.Linq;
using Equinox.Domain.Core.Events;
using Equinox.Domain.Interfaces;

namespace Equinox.Application.EventSourcedNormalizers
{
    //public class CustomerHistory
    //{
    //    public static IList<EventHistoryData> HistoryData { get; set; }

    //    public static IList<EventHistoryData> ToJavaScriptCustomerHistory(IList<StoredEvent> storedEvents, ISerializer serializer)
    //    {
    //        HistoryData = new List<EventHistoryData>();
    //        CustomerHistoryDeserializer(storedEvents, serializer);

    //        var sorted = HistoryData.OrderBy(c => c.When);
    //        var list = new List<EventHistoryData>();
    //        var last = new EventHistoryData();

    //        foreach (var change in sorted)
    //        {
    //            var jsSlot = new EventHistoryData
    //            {
    //                Id = change.Id == Guid.Empty.ToString() || change.Id == last.Id
    //                    ? ""
    //                    : change.Id,
    //                Name = string.IsNullOrWhiteSpace(change.Name) || change.Name == last.Name
    //                    ? ""
    //                    : change.Name,
    //                Email = string.IsNullOrWhiteSpace(change.Email) || change.Email == last.Email
    //                    ? ""
    //                    : change.Email,
    //                BirthDate = string.IsNullOrWhiteSpace(change.BirthDate) || change.BirthDate == last.BirthDate
    //                    ? ""
    //                    : change.BirthDate.Substring(0,10),
    //                Action = string.IsNullOrWhiteSpace(change.Action) ? "" : change.Action,
    //                When = change.When,
    //                Who = change.Who
    //            };

    //            list.Add(jsSlot);
    //            last = change;
    //        }
    //        return list;
    //    }

    //    private static void CustomerHistoryDeserializer(IEnumerable<StoredEvent> storedEvents, ISerializer serializer)
    //    {
    //        foreach (var e in storedEvents)
    //        {
    //            var slot = new EventHistoryData();
    //            dynamic values;

    //            switch (e.MessageType)
    //            {
    //                case "CustomerRegisteredEvent":
    //                    values = serializer.DeserializeFromString<dynamic>(e.Data);
    //                    slot.BirthDate = values["BirthDate"];
    //                    slot.Email = values["Email"];
    //                    slot.Name = values["Username"];
    //                    slot.Action = "Registered";
    //                    slot.When = values["Timestamp"];
    //                    slot.Id = values["Id"];
    //                    slot.Who = e.User;
    //                    break;
    //                case "CustomerUpdatedEvent":
    //                    values = serializer.DeserializeFromString<dynamic>(e.Data);
    //                    slot.BirthDate = values["BirthDate"];
    //                    slot.Email = values["Email"];
    //                    slot.Name = values["Username"];
    //                    slot.Action = "Updated";
    //                    slot.When = values["Timestamp"];
    //                    slot.Id = values["Id"];
    //                    slot.Who = e.User;
    //                    break;
    //                case "CustomerRemovedEvent":
    //                    values = serializer.DeserializeFromString<dynamic>(e.Data);
    //                    slot.Action = "Removed";
    //                    slot.When = values["Timestamp"];
    //                    slot.Id = values["Id"];
    //                    slot.Who = e.User;
    //                    break;
    //            }
    //            HistoryData.Add(slot);
    //        }
    //    }
    //}
}