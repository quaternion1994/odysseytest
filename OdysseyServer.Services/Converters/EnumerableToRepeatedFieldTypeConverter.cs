﻿using AutoMapper;
using Google.Protobuf.Collections;
using System.Collections.Generic;

namespace OdysseyServer.Services.Converters
{
    public class EnumerableToRepeatedFieldTypeConverter<TITemSource, TITemDest> : ITypeConverter<IEnumerable<TITemSource>, RepeatedField<TITemDest>>
    {
        public RepeatedField<TITemDest> Convert(IEnumerable<TITemSource> source, RepeatedField<TITemDest> destination, ResolutionContext context)
        {
            destination = destination ?? new RepeatedField<TITemDest>();
            foreach (var item in source)
            {
                destination.Add(context.Mapper.Map<TITemDest>(item));
            }
            return destination;
        }
    }
}
