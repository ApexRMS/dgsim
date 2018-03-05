'*********************************************************************************************
' DG-Sim: A SyncroSim Module for simulating demographic population models.
'
' Copyright © 2007-2018 Apex Resource Management Solution Ltd. (ApexRMS). All rights reserved.
'
'*********************************************************************************************

Module Enums

    Enum Gender
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
