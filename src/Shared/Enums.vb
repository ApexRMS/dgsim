'**********************************************************************************************
' dgsim: SyncroSim Base Package for simulating demographics of wildlife populations.
'
' Copyright © 2007-2019 Apex Resource Management Solutions Ltd. (ApexRMS). All rights reserved.
'
'**********************************************************************************************

Module Enums

    Enum Sex
        Male = 0
        Female = 1
    End Enum

    Enum PosteriorDistribution
        Harvest = 0
        Mortality = 1
        Offspring = 2
    End Enum

    Enum AnnualHarvestSpecification
        AbsoluteNumber = 0
        PercentageOfCohort = 1
        PercentageOfPopulation = 2
    End Enum

End Module
